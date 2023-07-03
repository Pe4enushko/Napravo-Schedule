namespace ScheduleAPI.Models.Responses
{
    public struct ClassReadable
    {
        public int IdClass { get; set; }

        public string SubjectTitle { get; set; }

        public string TeacherName { get; set; }

        public string GroupTitle { get; set; }

        /// <summary>
        /// Time from 0:00 in minutes
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// index of day of week
        /// </summary>
        public int DayOfWeek { get; set; }
    }
}
