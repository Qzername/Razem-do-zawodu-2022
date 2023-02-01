using CalendarioAPI.Model;
using ForesterAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task = CalendarioAPI.Model.Task;

namespace CalendarioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string token, int id)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID={loginToken.ID} AND ID={id}");

            if (tasks.Length == 0)
                return StatusCode(404);

            var schedules = SQLDatabase.Select<Schedule>($"SELECT * FROM Schedules WHERE TaskID={id}");

            return Ok(JSONManager.Serialize(schedules));
        }

        [HttpPost]
        public IActionResult Post([FromHeader] string token, [FromBody] ScheduleCreation schedule)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID={loginToken.ID} AND ID={schedule.TaskID}");

            if (tasks.Length == 0)
                return StatusCode(404);

            if (schedule.DateBegin < DateTime.Today.Ticks)
                return StatusCode(400);

            if (schedule.DateEnd == null)
                schedule.DateEnd = 0;

            SQLDatabase.NoReturnQuery($"INSERT INTO Schedules(TaskID, DateBegin, DateEnd) VALUES({schedule.TaskID}, {schedule.DateBegin}, {schedule.DateEnd})");

            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch([FromHeader] string token, [FromBody] Schedule schedule)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID={loginToken.ID} AND ID={schedule.TaskID}");

            if (tasks.Length == 0)
                return StatusCode(404);

            var schedules = SQLDatabase.Select<Schedule>($"SELECT * FROM Schedules WHERE TaskID={schedule.ID}");

            if (schedules.Length == 0)
                return StatusCode(404);

            string query = "UPDATE Accounts SET ";

            if (schedules[0].PriorityID != schedule.PriorityID)
                query += $"ProrityID={schedule.PriorityID},";

            if (schedule.DateBegin < DateTime.Today.Ticks)
                query += $"DateBegin={schedule.DateBegin},";

            if (schedule.DateEnd < DateTime.Today.Ticks)
                query += $"DateEnd={schedule.DateEnd},";

            if (query[^1] != ',')
                return StatusCode(406);

            query = query.Remove(query.Length - 1);
            query += $" WHERE TaskID={schedule.TaskID}";

            SQLDatabase.NoReturnQuery(query);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string token, int id)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID={loginToken.ID} AND ID={id}");

            if (tasks.Length == 0)
                return StatusCode(404);

            var schedules = SQLDatabase.Select<Schedule>($"SELECT * FROM Schedules WHERE TaskID={id}");

            if (schedules.Length == 0)
                return StatusCode(404);

            SQLDatabase.NoReturnQuery($"DELETE FROM Schedules WHERE ID={id}");

            return Ok();
        }
    }
}
