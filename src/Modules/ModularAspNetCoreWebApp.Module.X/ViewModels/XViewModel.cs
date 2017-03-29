using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ModularAspNetCoreWebApp.Module.X.ViewModels
{
    public class XViewModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; } = "Module X Message";
    }
}
