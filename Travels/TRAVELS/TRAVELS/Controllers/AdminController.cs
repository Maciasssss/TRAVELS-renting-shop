using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TRAVELS.Data;
using TRAVELS.Models;


namespace TRAVELS.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
      
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> SearchUsers(string searchTerm)
        {
            var users = string.IsNullOrEmpty(searchTerm) ? Enumerable.Empty<IdentityUser>() : _userManager.Users.Where(u => u.Email.Contains(searchTerm)).ToList();
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new AdminViewModel
            {
                Users = users,
                Roles = roles
            };

            return View("AdminPanel", model);
        }
        public IActionResult AdminPanel(string searchTerm = null)
        {
            var users = string.IsNullOrEmpty(searchTerm) ? new List<User>() : _userManager.Users.Where(u => u.Email.Contains(searchTerm)).ToList();
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new AdminViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return RedirectToAction("SearchUsers");
        }

       
    }
    public class AdminViewModel
    {
        public IEnumerable<IdentityUser> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
