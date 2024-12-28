using Microsoft.AspNetCore.Identity;
using InvoiceApp.Models;
using InvoiceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<Employee> GetCurrentEmployee()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            if (user == null || user.EmployeeId == 0)
            {
                return new Employee { FullName = "No Employee" };
            }
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == user.EmployeeId);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _httpContextAccessor.HttpContext.Response.Redirect("/");
        }


    }
}
