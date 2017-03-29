using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularAspNetCoreWebApp.Core.Domain.Models {
    public abstract class Entity : Entity<long> {
    }

    public class Entity<TId> : IEntity<TId>  {
        public TId Id { get; protected set; }
    }
}
