using ApiProject.Data;
using ApiProject.Models;
using ApiProject.unitofwork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<SchoolContext>(
                op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SchoolContext>()
                .AddDefaultTokenProviders();






            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:5150", "http://127.0.0.1:5500","http://localhost:3000","null")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // JWT
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; 
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                    roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

                var adminEmail = "ahmed@gmail.com";
                var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
                if (admin == null)
                {
                    admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, FullName = "Super Admin" };
                    userManager.CreateAsync(admin, "Ahmed@123").GetAwaiter().GetResult();
                    userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
                }
            }


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowLocalhost");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
