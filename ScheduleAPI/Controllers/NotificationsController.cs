using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ScheduleAPI.DB;
using ScheduleAPI.Logic;
using ScheduleAPI.Models;
using ScheduleAPI.Models.POST;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        ScheduleContext db;
        public NotificationsController(ScheduleContext db)
        {
            this.db = db;
        }

        // GET: api/<NotificationsController>/new/5
        [HttpGet("unseen")]
        public ActionResult<Notification[]> Get(int studentId)
        {
            if (!db.Students.Any(s => s.IdStudent == studentId))
                return BadRequest("No such student");

            var data = db.Notifications
                .Where(n => n.IdStudent == studentId 
                            && n.IsChecked == false)
                .ToArray();

            return Ok(data);
        }

        // POST api/<NotificationsController>
        [HttpPost("new")]
        public ActionResult Post([FromBody] NotificationPost value)
        {
            var notification = new Notification()
            {
                Header = value.Header,
                Message = value.Message,
                IdStudent = value.IdStudent,
                Moment = DateTime.Now,
                IsChecked = false
            };
            if (!ModelValidator.Validate(notification))
                return BadRequest("Bad data");

            db.Notifications.Add(notification);
            try
            {
                db.SaveChangesAsync();
            }
            catch (DbUpdateException exc)
            {
                return BadRequest(exc);
            }
            return Ok();
        }
    }
}
