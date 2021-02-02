using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Latest_News.Models;
using Latest_News.Models.Rep;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Latest_News
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<Repository<Categories>, Rep_Cat>();
            services.AddScoped<Repository<Commentaire>, Rep_Commentaire>();
            services.AddScoped<Repository<Latest_News.Models.Latest_News>, Rep_News>();
            services.AddScoped<Repository<Video_News>, Rep_VideoNews>();
            services.AddScoped<Repository<Publiciter>, Rep_Publiciter>();

            services.AddDbContext<ApplicationDbContext>(
            option => option.UseSqlServer(Configuration.GetConnectionString("ConxNews"))
            );
            services.AddIdentity<AppUsers, IdentityRole>().
           AddEntityFrameworkStores<ApplicationDbContext>().
           AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
