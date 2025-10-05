using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i=>new SmtpEmailSender(
builder.Configuration["EmailSender:Host"],
builder.Configuration.GetValue<int>("EmailSender:Port"),
builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
builder.Configuration["EmailSender:Username"],
builder.Configuration["EmailSender:Password"])
);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");
options.UseSqlite(connectionString);
});

builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:database"]));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
    // options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyz";

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Register";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(         // yukardakiyle aynı
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

SeedData.TestUser(app);

app.Run();
