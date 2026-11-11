using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gharshare.DataAccessLayer;
using Gharshare.DataAccessLayer.DbInitializer;
using AspNetCore.SEOHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.DataAccessLayer.Data.Infrastructure.Repository;
using Gharshare.CommonHelper;
using Gharshare.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Gharshare.com.pk.Hubs;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Added Account Confirmation option. pass 'RequireConfirmedAccount = true', if email confirmation needed during sign up.
//the client has asked to skip email confirmation during sign up, hence passing 'RequireConfirmedAccount = false'. Set to
// true when email confirmation required.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<Utility, Utility>();
//Dependecny injection for OTP/SMS upon Sign up
builder.Services.Configure<TwilioOptions>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddScoped<TwilioService>();
//builder.Services.AddScoped<ITempDataDictionary, TempDataDictionary>();
//builder.Services.AddScoped<DbContext, ApplicationDBContext>();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(SessionCheckFilterAttribute)); // Register the custom filter attribute globally
});                                                           // to check if session has been timed out.
builder.Services.AddMemoryCache();

//Dependency injection for ListingMatchingService background service to perform periodic tasks
//builder.Services.AddHostedService <ListingMatchingService>();
//builder.Services.AddHostedService<HostedService>();
//End of Dependency injection for ListingMatchingService background service to perform periodic tasks

builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<EmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
    //options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.AccessDeniedPath = $"/AccessDenied";
    //options.LoginPath = $"/Identity/Account/Login";
    options.LoginPath = $"/login";
    options.LogoutPath = $"/Identity/Account/Logout";
    //options.LogoutPath = $"/logout";

}
);

// Set the default culture to en-GB
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "usersession";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}
    );

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "1097873761205-2ubpe5eerhqe92oo8ukn8v6a50p7vb11.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    });

//builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
//{
//    facebookOptions.AppId = "1061001825085775";
//    facebookOptions.AppSecret = "fe16dac8af4572e1399145317eac4d4f";
//});

var app = builder.Build();
// Configure middleware for browser caching of static files
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Set caching headers for static assets (adjust values as needed)
        ctx.Context.Response.Headers["Cache-Control"] = "public, max-age=604800"; // 1 week
        ctx.Context.Response.Headers["Expires"] = DateTime.UtcNow.AddDays(7).ToString("R");
    }
});
 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseSession();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseXMLSitemap(app.Environment.ContentRootPath);
    app.UseRobotsTxt(app.Environment.ContentRootPath);

    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");

    app.UseRouting();

    app.UseAuthentication(); ;

    app.UseAuthorization();

    app.MapRazorPages();
    app.MapHub<ChatHub>("/chatHub");
    //app.MapHub<AdminChatHub>("/AdminchatHub");
    app.MapHub<AdminChatHub>("/AdminChatHub", options =>
    {
        options.Transports = HttpTransportType.WebSockets;
        options.LongPolling.PollTimeout = TimeSpan.FromMinutes(3);
    });

    DataSeeding();

    app.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");

    app.MapControllerRoute(
        name: "portal",
        pattern: "{area:exists}/{controller=Portal}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=index}/{id?}");
    //app.MapAreaControllerRoute(
    //    name: "identity",
    //    areaName: "identity",
    //    pattern: "identity/{page=login}/{id?}");


    //app.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=ApplyForTenancy}/{id?}");


    app.Run();

    void DataSeeding()
    {
        using (var scope = app.Services.CreateScope())
        {
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            DbInitializer.Initialize();
        }
    }
