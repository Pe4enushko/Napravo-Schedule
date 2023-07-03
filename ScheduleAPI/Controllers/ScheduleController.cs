﻿using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.DB;
using ScheduleAPI.Models;
using ScheduleAPI.Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        ScheduleContext db;
        public ScheduleController()
        {
            db = new ScheduleContext();
        }

        // GET: api/<ScheduleController>
        [HttpGet("{groupTitle}")]
        public ActionResult<ClassReadable[]> Get(string groupTitle)
        {
            try
            {
                int idGroup = db.Groups.First(g => g.Title == groupTitle).IdGroup;
                Class[] result = db.Classes.Where(cl => cl.IdGroup == idGroup).ToArray();
                ClassReadable[] classes = new ClassReadable[result.Length];

                int len = result.Length;

                for (int i = 0; i < len; i++)
                {
                    var current = result[i];
                    classes[i] = new ClassReadable()
                    {
                        IdClass = current.IdClass,
                        GroupTitle = current.IdGroupNavigation.Title,
                        SubjectTitle = current.IdSubjectNavigation.Title,
                        TeacherName = current.IdTeacherNavigation.Fullname,
                        DayOfWeek = current.DayOfWeek,
                        Time = current.Time
                    };
                }

                return Ok(classes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}