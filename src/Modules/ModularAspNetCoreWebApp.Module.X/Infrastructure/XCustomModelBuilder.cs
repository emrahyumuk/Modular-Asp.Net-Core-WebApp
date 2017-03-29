using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularAspNetCoreWebApp.Core;
using ModularAspNetCoreWebApp.Module.X.Models;

namespace ModularAspNetCoreWebApp.Module.X.Infrastructure {
    public class XCustomModelBuilder : ICustomModelBuilder {
        public void Build(ModelBuilder modelBuilder) {
            modelBuilder.Entity<XItem>().Property(x => x.Name).HasColumnName("XName");
        }
    }
}
