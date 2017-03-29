using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ModularAspNetCoreWebApp.Core.Domain.Models;

namespace ModularAspNetCoreWebApp.Module.Main.Models {
    public sealed class Role : IdentityRole<long>, IEntity<long> {
        public Role(string name) {
            Name = name;
        }
    }
}
