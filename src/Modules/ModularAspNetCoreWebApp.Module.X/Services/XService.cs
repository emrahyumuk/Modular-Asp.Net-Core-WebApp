using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularAspNetCoreWebApp.Module.X.Services {
    public class XService : IXService {
        public string GetServiceName() {
            return "X Service";
        }
    }
}
