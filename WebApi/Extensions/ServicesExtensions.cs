using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Abstract;
using Repositories.EFCore;
using Services;
using Services.Abstract;
using Services.Concrete;
using System.Reflection.Metadata;
using System.Text;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("WebApi")
            ));

        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;//rakam istiyor muyuz
                options.Password.RequireLowercase = true;//küçük hardf 
                options.Password.RequireUppercase = true;
               options.Password.RequireNonAlphanumeric = true;//özel karekter
                //options.Password.RequiredLength = 10;//uzunluk
                options.User.RequireUniqueEmail = true;//bir emaille tek giriş

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();

        }

        public static void  ConfigureJwt(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("JwtSettings");
            var secretKey = jwtSetting["secretKey"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting["validIssuer"],
                    ValidAudience = jwtSetting["validAudience"],
                    //TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASEFRFDDWSDRGYHF")),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                   // ClockSkew = TimeSpan.Zero

                };
            });

        }
        public static void ConfigureLoggerService(this IServiceCollection services) 
        {
           services.AddSingleton<ILoggerService,LoggerManager>();
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger=>
            {
                swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Description",
                    Name = "Name",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme="Bearer"
                }) ;

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                                 new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        },

                        Name="Bearer"
                    },
                                 new List<String>()
                    }
                });
            });
        } 
    
    
    
    }
}
