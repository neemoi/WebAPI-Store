using Application.MappingProfile.Admin;
using Application.Services.Implementations.Admin;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPIKurs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<WebsellContext>(options =>
            {
                options.UseMySql("server=localhost;database=WebSell;uid=admin;pwd=admin",
                    ServerVersion.Parse("8.0.33-mysql"));
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Registering Scoped Services
            builder.Services.AddAutoMapper(typeof(MappingAccount), typeof(MappingRoles));

            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            //Identity Configuration
            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<WebsellContext>()
            .AddRoles<IdentityRole>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}