using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using CurbashaApi.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using CurbashaApi.Models;
using CurbashaApi.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CurbashaApiContextConnection") ?? throw new InvalidOperationException("Connection string 'CurbashaApiContextConnection' not found.");

builder.Services.AddDbContext<CurbashaApiContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CurbashaApiContext>();



// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();


builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ICustomEmailSender,EmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CurbashaApiContext>();
    context.Database.Migrate();
    SeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.MapRazorPages();

app.Run();

