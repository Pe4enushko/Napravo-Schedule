using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.DB;

namespace ScheduleAPI.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        ScheduleContext db;
        public AuthController(ScheduleContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult Get(string login, string password)
        {
            return db.Students
                .Any(s => s.Login == login && s.Password == password) ? Ok("success") : BadRequest("fail");
        }
    }
}
