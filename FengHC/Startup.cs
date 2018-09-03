using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FengHC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //添加mcc服务
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //添加访问本地文件所需的服务
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();//提供 Web 根目录内的文件

            //配置web根目录外的文件(可以直接通过浏览器get到的目录，暂时取消不要了，改为不开放)
            //    app.UseStaticFiles(new StaticFileOptions
            //    {
            //        FileProvider = new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), "WWWResources")),
            //        RequestPath = "/WWWResources"
            //    });

            app.UseCookiePolicy();

            //添加路由
            app.UseMvc(routes =>
            {
                routes.MapRoute("blog", "blog/{*article}",
                    defaults: new { controller = "Blog", action = "Article" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            // app.usemi

            Main.Init();//初始化

            //app.Run(async (context) =>
            //{
            //     await context.Response.WriteAsync("Hello World!");
            //});
        }


        public void Configure2(IApplicationBuilder app, IHostingEnvironment env, ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute("blog", "blog/{*article}",
                    defaults: new { controller = "Blog", action = "Article" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });


            logger.LogDebug("初始化完成！");

            //app.Run(async (context) =>
            //{
            //     await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
