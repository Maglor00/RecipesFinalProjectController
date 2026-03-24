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
    public class ProfileModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public ProfileModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

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

        [BindProperty]
        public IFormFile? AvatarFile { get; set; }

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

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                string avatarUrl = await SaveAvatarAsync();

                Users updatedUser = UsersService.UpdateProfile(userId, FirstName, LastName, Username, avatarUrl);

                await RefreshAuthCookie(updatedUser);

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

        private async Task<string> SaveAvatarAsync()
        {
            if (AvatarFile == null || AvatarFile.Length <= 0)
                return string.Empty;

            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "avatars");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string extension = Path.GetExtension(AvatarFile.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await AvatarFile.CopyToAsync(stream);
            }

            return $"/uploads/avatars/{fileName}";
        }

        private async Task RefreshAuthCookie(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? ""),
                new Claim("FirstName", user.FirstName ?? ""),
                new Claim("LastName", user.LastName ?? ""),
                new Claim("is_admin", user.IsAdmin.ToString()),
                new Claim("avatar_url", user.AvatarUrl ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );
        }
    }
}
