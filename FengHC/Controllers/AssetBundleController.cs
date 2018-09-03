using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using FengHC.UnitySystem.ABManage;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FengHC.Controllers
{
    /// <summary>
    /// AssetBundle接口
    /// </summary>
    [Route("api/assetbundle")]
    public class AssetBundleController : Controller
    {
        //为了在控制器中请求一个provider，需要在控制器的构造函数中指定类型参数并赋值给本地属性。
        //之后你就可以在你的动作器方法中使用本地实例了。
        private readonly IFileProvider currentFileProvider;
        public AssetBundleController(IFileProvider fileProvider)
        {
            currentFileProvider = fileProvider;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("GetCacheFile")]
        public IActionResult GetCacheFile()
        {
            return Ok(AssetBundleManager.GetABCacheFileText(currentFileProvider));
        }

        [HttpGet("GetDepFile")]
        public IActionResult GetDepFile()
        {
            return Ok(AssetBundleManager.GetABDepFileText(currentFileProvider));
        }

        /// <summary>
        /// api/assetbundle/GetAssetBundle
        /// </summary>
        [HttpPost("GetAssetBundle")]
        [Consumes("application/octet-stream")]
        public IActionResult GetAssetBundle()
        {
            //ASP NET Core不允许我们仅仅通过方法参数以任何有意义的方式捕获“原始”数据。因此我们需要通过处理Request.Body来获取原始数据，然后反序列化它。
            //我们可以捕获原始的Request.Body并从原始缓冲区中读取参数。最简而有效的方法是接受不带参数的POST或PUT数据，然后从Request.Body读取原始数据：
            using (var ms = new MemoryStream(2048))
            {
                //await Request.Body.CopyToAsync(ms);
                Request.Body.CopyTo(ms);
                string abName = System.Text.Encoding.UTF8.GetString(ms.ToArray());

                var abData = AssetBundleManager.GetABFile(currentFileProvider, abName);
                return File(abData, "application/octet-stream");
            }
        }




        [HttpPost("GetAssetBundle2")]
        public async Task<byte[]> GetAssetBundle2()
        {
            //ASP NET Core不允许我们仅仅通过方法参数以任何有意义的方式捕获“原始”数据。因此我们需要通过处理Request.Body来获取原始数据，然后反序列化它。
            //我们可以捕获原始的Request.Body并从原始缓冲区中读取参数。最简而有效的方法是接受不带参数的POST或PUT数据，然后从Request.Body读取原始数据：
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                string abName = System.Text.Encoding.UTF8.GetString(ms.ToArray());

                var abData = AssetBundleManager.GetABFile(currentFileProvider, abName);
                return abData;
            }
        }



        [HttpPost("GetTextAssetBundle")]
        public async Task<IActionResult> GetTextAssetBundle()
        {
            //ASP NET Core不允许我们仅仅通过方法参数以任何有意义的方式捕获“原始”数据。因此我们需要通过处理Request.Body来获取原始数据，然后反序列化它。
            //我们可以捕获原始的Request.Body并从原始缓冲区中读取参数。最简而有效的方法是接受不带参数的POST或PUT数据，然后从Request.Body读取原始数据：
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                string getModel = System.Text.Encoding.UTF8.GetString(ms.ToArray());

                var cacheFileText = AssetBundleManager.GetABFile(currentFileProvider, getModel);
                return Ok(cacheFileText);
            }
        }


        /// <summary>
        /// 文件流的方式输出        
        /// </summary>
        public IActionResult DownLoad(string file)
        {
            var addrUrl = file;
            var stream = System.IO.File.OpenRead(addrUrl);
            string fileExt = GetFileExt(file);
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(stream, memi, Path.GetFileName(addrUrl));
        }

        private string GetFileExt(string file)
        {
            return null;
        }
    }
}
