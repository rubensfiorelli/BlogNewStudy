using Blog.Core.Application.Services;
using Blog.Core.Domain.Repositories;
using Blog.External.CrossCutting.Configuration;
using Blog.External.CrossCutting.Services;
using Blog.External.Data.DataContext;
using Blog.External.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.External.CrossCutting.DataModule
{
    public static class DataModule
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                 .AddRepositories()
                 .AddJwtServices()
                 .AddServices();


            services
                .AddDbContextPool<ApplicationDbContext>(opts => opts
                .UseQueryTrackingBehavior(QueryTrackingBehavior
                .NoTrackingWithIdentityResolution)
                .UseSqlServer(configuration
                .GetConnectionString("SQLConnection"), b => b
                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //static IDbConnection SqlConnection()
            //    => new SqlConnection("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True");

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ILoginRepository), typeof(LoginRepository));



            return services;

        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof (IUserService), typeof(UserService));
            services.AddScoped(typeof(ILoginService), typeof(LoginService));

            services.AddScoped(typeof(TokenService), typeof(TokenService));


            return services;

        }

        private static IServiceCollection AddJwtServices(this IServiceCollection services)
        {
            services.AddTransient<TokenService>();

            var key = Encoding.ASCII.GetBytes(ConfigurationJwt.JwtKey);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(dec =>
            {
                dec.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;

        }

    }
}
