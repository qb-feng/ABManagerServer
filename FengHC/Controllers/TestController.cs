using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FengHC
{
    [Route("qiubin")]
    public class TestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Ok("good");
        }

        [HttpGet("laogei")]
        public IActionResult GetLogger()
        {
            //return Ok("12321321");
            return Ok("傻逼老给····");
        }
    }
}
