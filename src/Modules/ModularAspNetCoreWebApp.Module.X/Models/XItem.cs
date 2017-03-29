using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModularAspNetCoreWebApp.Core.Domain.Models;

namespace ModularAspNetCoreWebApp.Module.X.Models {
    public class XItem : Entity {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
