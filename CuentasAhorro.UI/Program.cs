using CuentasAhorro.DependencyResolution;
using CuentasAhorro.Services;
using CuentasAhorro.Services.Interface;
using CuentasAhorro.UI.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddDbContext(config);
builder.Services.AddIdentity();
builder.Services.AddPersistence();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuthenticatedService, AuthenticatedService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/account/login";
    options.LogoutPath = $"/account/logout";
});

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

DependencyInjection.Initialize(app.Services);

app.Run();