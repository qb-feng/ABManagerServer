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

            //���mcc����
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //��ӷ��ʱ����ļ�����ķ���
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
            app.UseStaticFiles();//�ṩ Web ��Ŀ¼�ڵ��ļ�

            //����web��Ŀ¼����ļ�(����ֱ��ͨ�������get����Ŀ¼����ʱȡ����Ҫ�ˣ���Ϊ������)
            //    app.UseStaticFiles(new StaticFileOptions
            //    {
            //        FileProvider = new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), "WWWResources")),
            //        RequestPath = "/WWWResources"
            //    });

            app.UseCookiePolicy();

            //���·��
            app.UseMvc(routes =>
            {
                routes.MapRoute("blog", "blog/{*article}",
                    defaults: new { controller = "Blog", action = "Article" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            // app.usemi

            Main.Init();//��ʼ��

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


            logger.LogDebug("��ʼ����ɣ�");

            //app.Run(async (context) =>
            //{
            //     await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
