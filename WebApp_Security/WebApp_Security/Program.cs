var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MyPersonalCookie").AddCookie("MyPersonalCookie", options =>
{
    options.Cookie.Name = "MyPersonalCookie"; //The cookie must be stored in a constant !!!
    //options.LoginPath = "/AccountTest/Login"; this setup the login page path, if you don't set this, the default path is /Account/Login
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeFromHRDepartment", policy =>
    {
        policy.RequireClaim("Department", "HR");
    });
});

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

app.Run();
