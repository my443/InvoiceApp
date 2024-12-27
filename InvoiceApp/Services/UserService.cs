using Microsoft.AspNetCore.Identity;
using InvoiceApp.Models;

namespace InvoiceApp.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _httpContextAccessor.HttpContext.Response.Redirect("/");
        }


    }
}
