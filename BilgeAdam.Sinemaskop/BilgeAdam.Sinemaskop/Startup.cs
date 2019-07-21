using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilgeAdam.Sinemaskop.Connection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace BilgeAdam.Sinemaskop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration); //Appsettings.json içerisindeki veriler Appsettings adındaki class propertylerine maplensin. Dolayısı ile DI aracılığı ile tüm classslaqrdan erişilebilr

            //TODO: Bknz: SinContext OnConfiguring methodu
            //Yöntem 1
            //services.AddDbContext<SinContext>(options =>
            //{
            //    //var configuration = new ConfigurationBuilder()
            //    //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    //    .AddJsonFile("appsettings.json")
            //    //    .Build();
            //    //options.UseSqlServer(configuration.GetSection("ConnectionStrings:SinContext").Path);
            //});

            //TODO: INotifyPropertyChanged => MVVM .NET interface'i okuyun :)
            services.AddDbContext<SinContext>();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
