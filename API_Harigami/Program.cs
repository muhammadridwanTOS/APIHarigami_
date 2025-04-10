//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//==================================================
// Add services to the container.
//==================================================
builder.Services.AddControllers();

//==================================================
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//==================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//==================================================
// JWT Authentication
//==================================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    o.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }
    };
#pragma warning restore CS8604 // Possible null reference argument.
});


//==================================================
// Add configuration from appsettings.json
//==================================================
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
//==================================================

var app = builder.Build();
//==================================================
// Configure the HTTP request pipeline.
//==================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//IConfiguration configuration = app.Configuration;

//IWebHostEnvironment environment = app.Environment;

app.MapControllers();

app.Run();