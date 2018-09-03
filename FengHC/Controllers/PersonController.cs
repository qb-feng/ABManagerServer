using Microsoft.AspNetCore.Mvc;
namespace FengHC
{
    /*
    为MVC定义路由有两种方式：使用IRouteBuilder或者使用基于属性标签的路由。针对Rest，最好还是使用基于属性标签的方式。
    路由属性标签可以标注在Controller或者Action方法上.
     */


    /// <summary>
    /// person 类的controller类
    /// 默认情况下Controller放在ASP.NET Core项目的Controllers目录下。
    /// 在ASP.NET Core项目里可以通过多种方式来创建Controller，当然最建议的方式还是通过继承AspNetCore.Mvc.Controller这个抽象类来建立Controller。
    /// 通过继承Controller基类的方法来创建Controller还是有很多好处的，因为它提供了很多帮助方法，例如：Ok, NotFound, BadRequest等，它们分别对应HTTP的状态码 200, 404, 400；此外还有Redirect，LocalRedirect，RedirectToRoute，Json，File，Content等方法。
    /// </summary>
    // 在Controller上使用[Route]属性就定义了该Controller下所有Action的路由基地址
    // Controller类上标注的路由“api/[controller]”，其中[controller] 就代表该类的名字去掉结尾Controller的部分，也就是“api/person”。
    [Route("api/[controller]")]
    public class PersonController : Controller
    {

        public IActionResult Get()
        {
            return Ok(new PersonController());
        }

        /// <summary>
        /// Action
        /// 在Controller里面，可以使用public修饰符来定义Action，通常会带有参数，可以返回任何类型，但是大多数情况下应该返回IActionResult。Action的方法名要么是以HTTP的动词开头，要么是使用HTTP动词属性标签，包括：[HttpGet], [HttpPut], [HttpPost], [HttpDelete], [HttpHead], [HttpOptions], [HttpPatch].
        /// </summary>
        [HttpGet]
        public IActionResult FindPerson(int id)
        {
            return null;
        }


        /// <summary>
        /// 其中某个方法名如果恰好是以HTTP的动词开头，那么可以通过标注 [NonAction] 属性来表示这个方法不是Action。
        /// </summary>
        [NonAction]
        public IActionResult GetTime()
        {
            return Ok(System.DateTime.Now);
        }

        #region 基于属性标签的路由
        /// <summary>
        /// 每个Action可以包含一个或者多个相对的路由模板（地址），这些路由模板可以在[Http...]中定义,
        /// </summary>
        [HttpGet("first123/{id}")]
        public IActionResult FindFirstPerson()
        {
            return null;
        }

        /// <summary>
        /// 但是如果使用 ~ 这个符号的话，该Action的地址将会是绝对路由地址，也就是覆盖了Controller定义的基路由。
        /// </summary>
        [HttpPost("~/api111/people")]
        public IActionResult Post(PersonModel person)
        {
            return null;
        }
        #endregion

        #region 实体绑定
        //传入的请求会映射到Action方法的参数，可以实原始数据类型也可以是复杂的类型例如Dto（data transfer object）或ViewModel。这个把Http请求绑定到参数的过程叫做实体绑定

        /*
其中id参数是定义在路由里的，而name参数在路由里没有，但是仍然可以从查询参数中把name参数映射出来。
注意路由参数和查询参数的区别，下面这个URL里val1和val2是查询参数，它们是在url的后边使用?和&分隔：
/product?val1=2&val2=10
而针对上面的Action，下面这个URL的路由参数id就是123：
/api/Person/first/123
         */
        [HttpGet("first/{id}")]
        public IActionResult FindFirstPerson(int id, string name)
        {
            PersonModel person = new PersonModel
            {
                id = id,
                name = name,
            };

            return Ok(person);
        }




        /// <summary>
        /// 请求方法
        /// 1 可以使用查询参数：/api/find?id=1&name=Dave
        /// 2 如果POST Json数据：放到body里就是{"id":12,"name":"qiubin"},那么在Action里面得到的参数person的属性值都是null。这是因为这样的原始数据是包含在请求的Body里面，为了解决这个问题，你需要告诉Action从哪里获取参数，
        /// 针对这个例子就应该使用 [FromBody] 属性标签,如果提交的是表单数据，那么就应该使用[FromForm]:
        /// </summary>
        [HttpPost("~/find/People")]//url   /find/People
        public IActionResult FindPeople([FromBody]PersonModel person)
        {
            return Ok(person);
        }


        #endregion





    }
}