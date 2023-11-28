using Microsoft.EntityFrameworkCore;
using SeneakerKop.Data;
using Microsoft.AspNetCore.Identity;
using SeneakerKop.Models;
using SeneakerKop.Services;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});


builder.Services.AddRazorPages();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddSession(options =>
{
    // Set session timeout and other options if needed
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as per your requirement
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();






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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
