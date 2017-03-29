using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularAspNetCoreWebApp.Core.Domain.Models
{
    public interface IEntity<out TId>
    {
       TId Id { get; }
    }
}
