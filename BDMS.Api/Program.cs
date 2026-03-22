using BDMS.Api;
using BDMS.Application.Interfaces;
using BDMS.Application.Services;
using BDMS.Infrastructure.Data;
using BDMS.Infrastructure.RealTime;
using BDMS.Infrastructure.Repositories;
using BDMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Data;
using System.Net.WebSockets;
using System.Text;
using static QuestPDF.Helpers.Colors;
using BDMS.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
Console.WriteLine(jwtSettings["Key"]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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

builder.Services.AddDbContext<BDMSDbContext>(options =>
options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<DonorService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<DonationService>();
builder.Services.AddScoped<CertificateGenerator>();
builder.Services.AddScoped<HospitalService>();
builder.Services.AddScoped<IBloodInventoryRepository, BloodInventoryRepository>();
builder.Services.AddScoped<DashboardService>();
//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<INotificationService, SignalRNotificationService>();
builder.Services.AddSignalR();

var keyString = builder.Configuration["Jwt:Key"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("AUTH FAILED: " + context.Exception.ToString());
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("TOKEN VALIDATED SUCCESSFULLY");
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy
        =>
    {
        policy.WithOrigins("https://pulse-link.netlify.app").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    }
        );
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BDMSDbContext>();
    context.Database.Migrate();
    if (!context.Users.Any(u => u.Role == "Admin"))
    {
        context.Users.Add(new User
        {
            UserName = "admin",
            Email = "admin@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin"
        }
            );
    }

    //Hospitals
    if (!context.Hospitals.Any())
    {
        context.Hospitals.AddRange(
    new Hospital { Id = 1, Name = "Aster Medcity", City = "Kochi", Address = "Cheranallur, Kochi", ContactPhone = "0484-6699999" },
    new Hospital { Id = 2, Name = "Rajagiri Hospital", City = "Aluva", Address = "Rajagiri Valley Rd, Aluva", ContactPhone = "0484-2700600" },
    new Hospital { Id = 3, Name = "Amrita Institute of Medical Sciences", City = "Kochi", Address = "Ponekkara, Kochi", ContactPhone = "0484-2802000" },
    new Hospital { Id = 4, Name = "Lisie Hospital", City = "Kochi", Address = "Pettah, Kochi", ContactPhone = "0484-2662222" },
    new Hospital { Id = 5, Name = "Medical Trust Hospital", City = "Kochi", Address = "MG Road, Kochi", ContactPhone = "0484-2361400" }
            );
    }
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/api/notifications");
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
app.Run();


