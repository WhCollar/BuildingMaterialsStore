using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.MapProfiles;
using TsentrstroyAPI.Middlewares;
using TsentrstroyAPI.Model;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            Settings settings = new Settings();
            Configuration.Bind("Settings", settings);
            services.AddSingleton(settings);

            services.AddLogging();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContextPool<DatabaseContext>(o =>
            {
                o.UseNpgsql(Configuration.GetConnectionString("DatabaseConnectionString")).EnableSensitiveDataLogging();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {  
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.BearerKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
            });

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            SeedData.SetConfiguration(Configuration);

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<UploadFileHelper>();

            services.AddAutoMapper(typeof(AppMappingProfile));
            
            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            WebFiles.Initialize(env);
            
            if (env.IsDevelopment()) { }
            
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions());
            
            app.UseRouting();
            
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://cstroy72.ru", "http://www.cstroy72.ru");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}