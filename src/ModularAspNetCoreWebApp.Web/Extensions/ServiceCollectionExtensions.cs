using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularAspNetCoreWebApp.Core;
using ModularAspNetCoreWebApp.Core.Domain;
using ModularAspNetCoreWebApp.Module.Main.Infrastructure;
using ModularAspNetCoreWebApp.Module.Main.Models;

namespace ModularAspNetCoreWebApp.Web.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection LoadInstalledModules(this IServiceCollection services,
            IList<ModuleInfo> modules, IHostingEnvironment hostingEnvironment) {
            var moduleRootFolder = new DirectoryInfo(Path.Combine(hostingEnvironment.ContentRootPath, "Modules"));
            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders) {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (!binFolder.Exists) {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories)) {
                    Assembly assembly;
                    try {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex) {
                        if (ex.Message == "Assembly with same name is already loaded") {
                            // Get loaded assembly
                            assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));
                        }
                        else {
                            throw;
                        }
                    }

                    if (assembly.FullName.Contains(moduleFolder.Name)) {
                        modules.Add(new ModuleInfo {
                            Name = moduleFolder.Name,
                            Assembly = assembly,
                            Path = moduleFolder.FullName
                        });
                    }
                }
            }

            GlobalConfiguration.Modules = modules;
            return services;
        }
        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services, IList<ModuleInfo> modules) {
            var mvcBuilder = services.AddMvc()
                .AddRazorOptions(o => {
                    foreach (var module in modules) {
                        o.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(module.Assembly.Location));
                    }
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            foreach (var module in modules) {
                // Register controller from modules
                mvcBuilder.AddApplicationPart(module.Assembly);

                // Register dependency in modules
                var moduleInitializerType =
                    module.Assembly.GetTypes().FirstOrDefault(x => typeof(IModuleInitializer).IsAssignableFrom(x));
                if ((moduleInitializerType != null) && (moduleInitializerType != typeof(IModuleInitializer))) {
                    var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
                    moduleInitializer.Init(services);
                }
            }

            return services;
        }

        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfigurationRoot configuration) {
            services.AddDbContext<CustomDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
               x => x.MigrationsAssembly("ModularAspNetCoreWebApp.Web")));
            return services;
        }

        public static IServiceCollection AddCustomizedIdentity(this IServiceCollection services) {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<CustomDbContext, long>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceProvider Build(this IServiceCollection services,
            IConfigurationRoot configuration, IHostingEnvironment hostingEnvironment) {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
            foreach (var module in GlobalConfiguration.Modules) {
                builder.RegisterAssemblyTypes(module.Assembly).AsImplementedInterfaces();
            }

            builder.RegisterInstance(configuration);
            builder.RegisterInstance(hostingEnvironment);
            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }
}