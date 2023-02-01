using CalendarioAPI.Model;
using CalendarioAPI.Model.Calendar;
using ForesterAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using Task = CalendarioAPI.Model.Task;

namespace CalendarioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        [HttpGet("[action]/{day}")]
        public IActionResult Day([FromHeader] string token, long day)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            return Ok(JSONManager.Serialize(GetDay(day, loginToken.ID)));
        }

        [HttpGet("[action]/{week}")]
        public IActionResult Week([FromHeader] string token, long week)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            return Ok(JSONManager.Serialize(GetWeek(week, loginToken.ID)));
        }

        public Day GetDay(long day, int AccountID)
        {
            DateTime date = new DateTime(day);
            var dateStart = date.Date.Ticks;
            var dateEnd = date.Date.AddHours(24).Ticks;

            var tasks = SQLDatabase.Select<Task>($"SELECT Tasks.* FROM Tasks, Schedules WHERE Schedules.DateBegin>{dateStart} AND Schedules.DateBegin<{dateEnd} AND Tasks.ID=Schedules.TaskID");

            return new Day(tasks);
        }

        public Day[] GetWeek(long weekStart, int AccountID)
        {
            Day[] week = new Day[7];

            DateTime date = new DateTime(weekStart);
            date.AddDays(-Convert.ToInt32(date.DayOfWeek));

            for(int i = 0; i < week.Length;i++)
            {
                week[i] = GetDay(date.Ticks, AccountID);
                date.AddDays(1);
            }

            return week;
        }
    }
}
