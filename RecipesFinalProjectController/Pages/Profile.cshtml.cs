using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string CurrentPassword { get; set; } = string.Empty;

        [BindProperty]
        public string NewPassword { get; set; } = string.Empty;

        public Users CurrentUser { get; set; } = new();

        [TempData]
        public string Message { get; set; } = string.Empty;


        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            CurrentUser = UsersService.Retrieve(userId);
            FirstName = CurrentUser.FirstName;
            LastName = CurrentUser.LastName;
            Username = CurrentUser.Username;

            return Page();
        }

        public IActionResult OnPostUpdateProfile()
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                UsersService.UpdateProfile(userId, FirstName, LastName, Username);

                Message = "Profile updated successfully.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return OnGet();
            }
        }

        public IActionResult OnPostChangePassword()
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                UsersService.ChangePassword(userId, CurrentPassword, NewPassword);

                Message = "Password changed successfully.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return OnGet();
            }
        }
    }
}
