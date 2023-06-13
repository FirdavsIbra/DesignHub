using database;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Mapper;
using Repository.Repositories;
using Service.DTO;
using Service.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var configuration = builder.Configuration;
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Add the JWT bearer token authorization header
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer token",
    });

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddDbContext<ContextDb>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IChatRepository, ChatMessageRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDesignRepository, DesignRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
            ValidateIssuer = true, // Set to true if you want to validate the issuer
            ValidIssuer = configuration["Jwt:Issuer"], // Set the valid issuer
            ValidateAudience = true, // Set to true if you want to validate the audience
            ValidAudience = configuration["Jwt:Audience"], // Set the valid audience
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Handle authentication failure
                return Task.CompletedTask;
            },
            OnTokenValidated = async context =>
            {
                // Perform additional validation if required
                var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                var userId = await authService.GetCurrentUserId(context.Principal);

                // Check if the user exists or has necessary permissions
                if (userId == 0)
                {
                    context.Fail("Unauthorized");
                }
            }
        };
    });
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");

        // Add the authorization button to Swagger UI
        c.OAuthClientId("swagger");
        c.OAuthAppName("Your API - Swagger");
    });
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
