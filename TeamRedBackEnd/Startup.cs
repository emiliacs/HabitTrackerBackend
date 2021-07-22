using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TeamRedBackEnd
{
    public class Startup
    {
        readonly string MyCorsPolicies = "_myCorsPolicies";
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyCorsPolicies, builder =>
                {
                    if (_env.IsDevelopment())
                    {
                        builder.SetIsOriginAllowed(_ => true)
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();

                    }
                    else builder.WithOrigins("http://localhost:19006");

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

            services.AddScoped<Database.Repositories.IRepositoryWrapper, Database.Repositories.RepositoryWrapper>();

            services.AddSingleton<Services.IAuthService>(new Services.AuthService(
               Configuration.GetSection("JWTSettings").GetValue<string>("SecurityKey"),
               Configuration.GetSection("JWTSettings").GetValue<int>("AverageLifespan")
               ));

            services.AddScoped<Services.PasswordService>();
            
            services.Configure<DataObjects.MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<Services.IMailService, Services.MailService>();

            services.AddAutoMapper(typeof(Startup));

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

            app.UseCors(MyCorsPolicies);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
