using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ModularAspNetCoreWebApp.Module.Y.Controller {
    public class YController : Microsoft.AspNetCore.Mvc.Controller {

        public IActionResult Index() {
            return View();
        }
    }
}
