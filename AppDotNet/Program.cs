using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppDotNet.Data;
using AppDotNet.Entities;

 using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using System.Globalization;
using JsonBasedLocalization.Web;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });




builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
        new CultureInfo("de-DE")
    };

    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var connectionString = builder.Configuration.GetConnectionString("AppDotNetContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDotNetContextConnection' not found.");

builder.Services.AddDbContext<AppDotNetContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDotNetContext>();




// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
app.UsePathBase("/app");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

var supportedCultures = new[] { "en-US", "ar-EG", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseHttpsRedirection();




app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
