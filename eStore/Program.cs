var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options => options.LoginPath = "/auth/login");
builder.Services.AddHttpClient();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseAuthentication();
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();