using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int EmployeeId { get; set; }
    }

}
