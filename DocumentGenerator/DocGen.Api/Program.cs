using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;

using DocGen.Api.ModelBinders;
using DocGen.Core.Contracts;
using DocGen.Core.Services;
using DocGen.Data;

using static DocGen.Common.ApplicationGlobalConstants;

namespace DocGen.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var appCors = "_appCors";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(appCors,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7080/")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
            });

            builder.Services
                .AddControllers()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAuthorizationFilter());
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DocGenDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddIdentity<IdentityUser<string>, IdentityRole<string>>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            })
                .AddEntityFrameworkStores<DocGenDbContext>()
                .AddDefaultTokenProviders(); ;

            builder.Services.AddAuthorization();
            var jwtSecret = builder.Configuration["JwtSecret"];
            var key = Encoding.ASCII.GetBytes(jwtSecret!);
            builder.Services
                .AddAuthentication(options =>
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
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<SignInManager<IdentityUser<string>>>();
            builder.Services.AddScoped<UserManager<IdentityUser<string>>>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IModelFactory, ModelFactory>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(appCors);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

    internal class AutoValidateAntiforgeryTokenAuthorizationFilter : IFilterMetadata
    {
    }
}
