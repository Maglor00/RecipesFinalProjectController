using RecipesFinalProjectRepo.Implementations;
using RecipesFinalProjectRepo.Interface;
using RecipesFinalProjectServices.Implementations;
using RecipesFinalProjectServices.Interface;

namespace RecipesFinalProjectController
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IUsersRepo, UsersRepo>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IFavoritesRepo, FavoritesRepo>();
            builder.Services.AddScoped<IFavoritesService, FavoritesService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
