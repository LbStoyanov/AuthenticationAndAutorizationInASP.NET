using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace WebApp_Security.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();


            if (Credential.UserName == "admin" && Credential.Password == "admin")
            {
                var claims = new List<Claim>
                {
                      new(ClaimTypes.Role, "Admin"),
                      new(ClaimTypes.Email, "admin@stoyanov.com")
                };
                    

                var claimsIdentity = new ClaimsIdentity(claims, "MyPersonalCookie");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MyPersonalCookie", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();

        }
    }
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
