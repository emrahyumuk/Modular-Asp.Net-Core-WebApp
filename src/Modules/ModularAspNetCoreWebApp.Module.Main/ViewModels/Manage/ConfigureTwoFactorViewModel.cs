using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModularAspNetCoreWebApp.Module.Main.ViewModels.Manage
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}
