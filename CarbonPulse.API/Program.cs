using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarbonPulse.API.Interfaces;
using CarbonPulse.API.Services;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// ✅ Register Services
// ------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Add Swagger + JWT Authorize support ONLY (unchanged logic)
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthService, AuthService>();

// ------------------------------
// ✅ Safe JWT Setup with Logging
// ------------------------------
try
{
    var jwtKey = builder.Configuration["AppSettings:Token"];

    Console.WriteLine("🔐 JWT Key Loaded: " + (string.IsNullOrWhiteSpace(jwtKey) ? "❌ EMPTY OR NULL" : "✅ PRESENT"));

    if (string.IsNullOrWhiteSpace(jwtKey))
        throw new Exception("JWT Token key is missing or invalid in appsettings.json");

    var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    Console.WriteLine("✅ JWT Authentication configuration completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine("❌ JWT Setup Error: " + ex.Message);
    throw;
}

// ------------------------------
// ✅ EmissionService 
// ------------------------------
builder.Services.AddScoped<IEmissionService, EmissionService>();

// ------------------------------
// ✅ Build & Middleware
// ------------------------------
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("🚀 Application starting...");

app.Run();
