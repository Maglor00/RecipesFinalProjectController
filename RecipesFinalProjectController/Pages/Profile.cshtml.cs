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
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public Users CurrentUser { get; set; }

        [TempData]
        public string Message { get; set; }


        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            CurrentUser = UsersService.Retrieve(userId);

            FirstName = CurrentUser.FirstName;
            LastName = CurrentUser.LastName;
            Username = CurrentUser.Username;

            return Page();
        }

        public IActionResult OnPostUpdateProfile()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Users updatedUser = new Users
                {
                    Id = userId,
                    FirstName = FirstName,
                    LastName = LastName,
                    Username = Username
                };

                UsersService.UpdateProfile(updatedUser);

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
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

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
