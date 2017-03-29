using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModularAspNetCoreWebApp.Core;
using ModularAspNetCoreWebApp.Module.Main.Models;

namespace ModularAspNetCoreWebApp.Module.Main.Infrastructure {
    public class CustomModelBuilder : ICustomModelBuilder {
        public void Build(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .ToTable("Core_User");

            modelBuilder.Entity<Role>()
                .ToTable("Core_Role");

            modelBuilder.Entity<IdentityUserClaim<long>>(b => {
                b.HasKey(uc => uc.Id);
                b.ToTable("Core_UserClaim");
            });

            modelBuilder.Entity<IdentityRoleClaim<long>>(b => {
                b.HasKey(rc => rc.Id);
                b.ToTable("Core_RoleClaim");
            });

            modelBuilder.Entity<IdentityUserRole<long>>(b => {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("Core_UserRole");
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(b => {
                b.ToTable("Core_UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b => {
                b.ToTable("Core_UserToken");
            });
        }
    }
}
