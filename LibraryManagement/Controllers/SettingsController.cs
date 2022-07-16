using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    [Route("/settings/{action=Index}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly INotyfService _notyf;
        public SettingsController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            var settings = SettingsHelper.GetSettings();
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(SettingsObject settings)
        {
            SettingsHelper.SetSettings(settings);
            _notyf.Information("Settings have been updated");

            return RedirectToAction("Index", "Home");
        }
    }
}
