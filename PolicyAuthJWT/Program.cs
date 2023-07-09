using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PolicyAuthJWT.Config.Auth.JwtService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IJwtService, JwtService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = "this-is-a-very-secret-key";
    var issuer = "your_issuer";
    var audience = "your_audience";
    var expirationInMinutes = 60;
    var longExpirationInMinutes = 1440;

    return new JwtService(secretKey, issuer, audience, expirationInMinutes, longExpirationInMinutes);
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Set your token validation parameters (e.g., issuer, audience, validation checks, etc.)
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-a-very-secret-key"))
        };
    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy();
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    // Read JWT token from cookie
    var jwtToken = context.Request.Cookies["jwtToken"];

    // Set JWT token in request headers
    if (!string.IsNullOrEmpty(jwtToken))
    {
        context.Request.Headers.Add("Authorization", $"Bearer {jwtToken}");
    }

    await next.Invoke();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();



app.Run();
