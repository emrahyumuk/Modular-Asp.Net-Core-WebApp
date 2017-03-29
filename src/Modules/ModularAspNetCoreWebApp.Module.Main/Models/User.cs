using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ModularAspNetCoreWebApp.Core.Domain.Models;

namespace ModularAspNetCoreWebApp.Module.Main.Models {
    public class User : IdentityUser<long>, IEntity<long> {
        public User() {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public string FullName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
