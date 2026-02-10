using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices.Interface;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUsersService _usersService;

        public LoginModel(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Users user = _usersService.Login(UserName, Password);

            if (user == null)
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }

            HttpContext.Session.SetString(
                "LoggedInUser",
                JsonSerializer.Serialize(user)
                );

            return RedirectToPage("/Profile");
        }
    }
}
