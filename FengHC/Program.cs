using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace FengHC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //假设在项目的根目录有这样一个json文件, 在ASP.NET Core项目里我们可以使用IConfigurationRoot来使用该json文件作为配置文件, 
            //而IConfigurationRoot是使用ConfigurationBuilder来创建的:
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("ConfigJson/FirstConfig.json");
            var configuration = builder.Build();
            foreach (var v in configuration.AsEnumerable())
            {
                System.Console.WriteLine("key:" + v.Key + "value:" + v.Value);
            }


            CreateWebHostBuilder(args).ConfigureLogging((loger) =>
            {
                loger.AddConsole();
            }).Build().Run();



        }
        /// <summary>
        /// 我们使用了WebHost.CreateDefaultBuilder()方法, 这个方法的默认配置大约如下:
        /// 采用Kestrel服务器, 使用项目个目录作为内容根目录, 默认首先加载appSettings.json, 然后加载appSettings.{环境}.json. 还加载了
        /// 一些其它的东西例如环境变量, UserSecrect, 命令行参数. 然后配置Log, 会读取配置数据的Logging部分的数据, 使用控制台Log提供商
        /// 和Debug窗口Log提供商, 最后设置了默认的服务提供商.
        /// 使用Startup作为启动类.
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
