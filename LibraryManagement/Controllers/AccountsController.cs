using AspNetCoreHero.ToastNotification.Abstractions;
using LibraryManagement.Dtos;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryManagement.Controllers
{
    [Route("/account/{action}/{id?}")]
    public class AccountsController : Controller
    {
        private ApplicationDbContext _context;
        private INotyfService _notyf;
        public AccountsController(ApplicationDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthDto auth)
        {
            if (!ModelState.IsValid)
            {
                return View(auth);
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == auth.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Email does not exist");
                return View(auth);
            }

            if (user.Password != auth.Password)
            {
                ModelState.AddModelError("Password", "Password is incorrect ");
                return View(auth);
            }

            await SignIn(new List<Claim>() {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            });

            _notyf.Information($"Successfully signed in. Welcome {user.FullName}");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            _notyf.Information("Successfully logged out");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Settings()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction(nameof(AccessDenied));
            }

            var userid = Guid.Parse(User.Identity.Name);
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userid);

            if (user == null)
            {
                return Unauthorized();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Settings([Bind("Id,Email,Password,FirstName,LastName")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            _notyf.Information("Account settings updated");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SetRole(UserRole role)
        {
            await SignIn(new List<Claim>
            {
                new Claim(ClaimTypes.Role, role.ToString())
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignIn (List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(28),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        }

    }
}
