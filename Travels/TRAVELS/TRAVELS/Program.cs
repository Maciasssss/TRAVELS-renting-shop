using TRAVELS.Data;
using Microsoft.EntityFrameworkCore;
using TRAVELS.IRepositoryInterface;
using TRAVELS.Repository;
using TRAVELS.Models;
using TRAVELS.Iservices;
using TRAVELS.Services;
using TRAVELS.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Manage.Internal;

namespace TRAVELS
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TravelDBcontext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static async Task SeedRolesAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manager", "Member" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<TravelDBcontext>(options =>
                           options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                        services.ConfigureApplicationCookie(options =>
                        {
                            // Cookie settings
                            options.Cookie.HttpOnly = true;
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                            options.LoginPath = "/Identity/Account/Login";
                            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                            options.SlidingExpiration = true;
                        });

                        services.AddIdentity<User, IdentityRole>(options =>
                        {
                            // Password settings.
                            options.Password.RequireDigit = true;
                            options.Password.RequireLowercase = true;
                            options.Password.RequireNonAlphanumeric = true;
                            options.Password.RequireUppercase = true;
                            options.Password.RequiredLength = 6;
                            options.Password.RequiredUniqueChars = 1;

                            // Lockout settings.
                            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                            options.Lockout.MaxFailedAccessAttempts = 5;
                            options.Lockout.AllowedForNewUsers = true;

                            // User settings.
                            options.User.AllowedUserNameCharacters =
                                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                            options.User.RequireUniqueEmail = false;

                            // Sign-in settings.
                            options.SignIn.RequireConfirmedAccount = true;
                        })
                          .AddEntityFrameworkStores<TravelDBcontext>()
                          .AddDefaultTokenProviders()
                          .AddDefaultUI();

                        //authoryzacja
                        services.AddAuthorization(options =>
                        {
                            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                            options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
                            options.AddPolicy("Member", policy => policy.RequireRole("Member"));
                        });

                        services.AddControllersWithViews();
                        services.AddScoped<IReservationsRepository, ReservationsRepository>();
                        services.AddScoped<ITravelRepository, TravelRepository>();
                        services.AddScoped<IUsersRepository, UserRepository>();
                        services.AddScoped<IGuideRepository, GuideRepository>();
                        services.AddScoped<IReservationService, ReservationService>();
                        services.AddScoped<IUserService,UserService>();
                        services.AddScoped<IGuideService, GuideService>();
                        services.AddScoped<ITravelService, TravelService>();
                        services.AddRazorPages();
                        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                        services.AddScoped<IValidator<GuideViewModel>, GuideViewModelValidator>();
                        services.AddScoped<IValidator<ReservationViewModel>, ReservationViewModelValidator>();
                        services.AddScoped<IValidator<TravelViewModel>, TravelViewModelValidator>();
                        services.AddScoped<IValidator<UserViewModel>, UserViewModelValidator>();



                    });

                    webBuilder.Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();

                        app.UseRouting();

                        app.UseAuthentication();
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                            endpoints.MapControllerRoute(
                                 name: "travel",
                                 pattern: "{controller=Home}/{action=Wycieczki}/{id?}");
                            endpoints.MapControllerRoute(
                                 name: "Reservations",
                                 pattern: "{controller=travel}/{action=Reservations}/{id?}");
                            endpoints.MapControllerRoute(
                                 name: "Admin",
                                 pattern: "{controller=Admin}/{action=AdminPanel}/{id?}");
                            endpoints.MapRazorPages();

                        });
                    });
                });
    }
}
