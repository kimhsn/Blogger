using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppDotNet.Data;
using AppDotNet.Entities;





var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDotNetContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDotNetContextConnection' not found.");

builder.Services.AddDbContext<AppDotNetContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDotNetContext>();




// Add services to the container.
builder.Services.AddRazorPages();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
