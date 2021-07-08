using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:19006");
                                      builder.AllowAnyHeader();
                                  });
            });
            services.AddDbContext<Database.DatabaseContext>(options => options.UseNpgsql(Configuration.GetSection("DatabaseLogin").GetSection("EasyLog").Value));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (
                            Encoding.UTF8.GetBytes(Configuration.GetSection("JWTSettings").GetValue<string>("SecurityKey"))
                        ),
                        
                    };
                });
            services.AddScoped<Database.Repositroies.IUsersRepository, Database.Repositroies.UsersRepository>();
            services.AddScoped<Services.PasswordService>();

            services.AddSingleton<Services.IAuthService>(new Services.AuthService(
               Configuration.GetSection("JWTSettings").GetValue<string>("SecurityKey"),
               Configuration.GetSection("JWTSettings").GetValue<int>("AverageLifespan")
               ));

            services.Configure<ViewModels.MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<Services.IMailService, Services.MailService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.z
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
