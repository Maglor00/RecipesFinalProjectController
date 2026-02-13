using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices.Interface;
using System.Diagnostics.Contracts;

namespace RecipesFinalProjectController.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUsersService _usersService;

        public RegisterModel(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        
        public IActionResult OnPost()
        {
            try
            {
                Users user = new Users
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Username = Username,
                    Password = Password,
                    IsAdmin = false

                };

                _usersService.Create(user);

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
