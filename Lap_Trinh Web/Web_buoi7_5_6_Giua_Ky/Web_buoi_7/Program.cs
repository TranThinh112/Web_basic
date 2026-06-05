using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Web_buoi_7.Models;
using Web_buoi_7.Data;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<AppUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();
//tao role
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    if (!await roleManager.RoleExistsAsync("Student"))
        await roleManager.CreateAsync(new IdentityRole("Student"));

var userManager =
    scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    string email = "admin@gmail.com";
    string password = "Admin@123";

    var admin = await userManager.FindByEmailAsync(email);

    if (admin == null)
    {
        admin = new AppUser
        {
            UserName = email,
            Email = email
        };

        await userManager.CreateAsync(admin, password);

        await userManager.AddToRoleAsync(admin, "Admin");
    }
}



app.Run();
