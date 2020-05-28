using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using CustomerApplication.API.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using CustomerApplication.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Buffers;

namespace CustomerApplication.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            


            string connectionlocaldb = "Server=(localdb)\\mssqllocaldb;Database=DataContext-7b0e9a62-e8be-4ee9-afd5-1db974d89c13;Trusted_Connection=True;MultipleActiveResultSets=true";
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            string connectiondonau = "Data Source=donau.hiof.no;Initial Catalog=robertal;Persist Security Info=True;User ID=robertal;Password=9Bk7QzZZ";
#pragma warning restore CS0219 // Variable is assigned but its value is never used


            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(connectiondonau));

            /*services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString(connectionlocaldb)));*/
           services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable IDE0060 // Remove unused parameter
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
