using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.DB;
using ScheduleAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        ScheduleContext db;
        public InfoController(ScheduleContext db)
        {
            this.db = db;
        }
        // GET: api/<InfoController>
        [HttpGet("Group/{title}")]
        public ActionResult<Group> Get(string title)
        {
            if (!db.Groups.Any(g => g.Title == title))
                return BadRequest("No such group");
    
            return Ok(db.Groups.First(g => g.Title == title));
        }
    }
}
