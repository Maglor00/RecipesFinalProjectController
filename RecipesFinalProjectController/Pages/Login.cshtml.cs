using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Profile");
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Users user = UsersService.Login(UserName, Password);

                if (user == null)
                {
                    ErrorMessage = "Invalid login.";
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username ?? ""),
                    new Claim("FirstName", user.FirstName ?? ""),
                    new Claim("LastName", user.LastName ?? ""),
                    new Claim("is_admin", user.IsAdmin.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return RedirectToPage("/Profile");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
