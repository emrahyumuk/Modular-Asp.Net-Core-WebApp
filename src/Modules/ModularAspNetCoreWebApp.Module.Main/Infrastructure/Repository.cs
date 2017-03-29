using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularAspNetCoreWebApp.Core.Domain;
using ModularAspNetCoreWebApp.Core.Domain.Models;

namespace ModularAspNetCoreWebApp.Module.Main.Infrastructure {
    public class Repository<T> : Repository<T, long>, IRepository<T>
        where T : class, IEntity<long> {
        public Repository(CustomDbContext context) : base(context) {
        }
    }

    public class Repository<T, TId> : IRepository<T, TId> where T : class, IEntity<TId> {
        public Repository(CustomDbContext context) {
            Context = context;
            DbSet = Context.Set<T>();
        }

        protected DbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public void Add(T entity) {
            DbSet.Add(entity);
        }

        public void SaveChange() {
            Context.SaveChanges();
        }

        public IQueryable<T> Query() {
            return DbSet;
        }

        public void Remove(T entity) {
            DbSet.Remove(entity);
        }
    }
}
