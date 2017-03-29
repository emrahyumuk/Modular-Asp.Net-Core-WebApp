using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModularAspNetCoreWebApp.Core.Domain;
using ModularAspNetCoreWebApp.Module.X.Models;
using ModularAspNetCoreWebApp.Module.X.Services;
using ModularAspNetCoreWebApp.Module.X.ViewModels;

namespace ModularAspNetCoreWebApp.Module.X.Controllers {
    public class XController : Controller {
        private readonly IXService _xService;
        private readonly IRepository<XItem> _xRepository;

        public XController(IXService xService, IRepository<XItem> xRepository) {
            _xService = xService;
            _xRepository = xRepository;
        }

        public IActionResult Index() {
            ViewBag.ServiceData = _xService.GetServiceName();

            var xItem = new XItem { Name = "Name X", Description = "Decription X" };
            _xRepository.Add(xItem);
            _xRepository.SaveChange();

            var model = new XViewModel { Name = xItem.Name, Description = xItem.Description };

            return View(model);
        }
    }
}
