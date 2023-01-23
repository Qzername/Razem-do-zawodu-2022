using CalendarioAPI.Model;
using ForesterAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;
using Task = CalendarioAPI.Model.Task;

namespace CalendarioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromHeader] string token)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID=\"{loginToken.ID}\"");

            return Ok(JSONManager.Serialize(tasks));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader]string token, int id)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID=\"{loginToken.ID}\" AND ID={id}");

            if (tasks.Length == 0)
                return StatusCode(404);

            return Ok(JSONManager.Serialize(tasks[0]));
        }

        [HttpPost]
        public IActionResult Post([FromHeader] string token, [FromBody] Task task)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            if (string.IsNullOrEmpty(task.Name))
                return StatusCode(400);

            SQLDatabase.NoReturnQuery($"INSERT INTO Tasks(AccountID, Name, Description, IsCompleted) VALUES({loginToken.ID}, {task.Name}, {task.Description}, 0)");

            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch([FromHeader] string token, [FromBody] Task task)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID=\"{loginToken.ID}\" AND ID={task.ID}");

            if (tasks.Length == 0)
                return StatusCode(404);

            string query = "UPDATE Accounts SET ";

            if (!string.IsNullOrEmpty(task.Name))
                query += $"Name=\"{task.Name}\",";

            if (!string.IsNullOrEmpty(task.Description))
                query += $"Description=\"{task.Description}\",";

            if (tasks[0].IsCompleted != task.IsCompleted)
                query += $"IsCompleted={int.Parse(task.IsCompleted.ToString())},";

            if (query[^1] != ',')
                return StatusCode(406);

            query = query.Remove(query.Length - 1);
            query += $" WHERE ID={task.ID}";

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

            var tasks = SQLDatabase.Select<Task>($"SELECT * FROM Tasks WHERE AccountID=\"{loginToken.ID}\" AND ID={id}");

            if (tasks.Length == 0)
                return StatusCode(404);

            SQLDatabase.NoReturnQuery($"DELETE FROM Tasks WHERE ID={id}");

            return Ok();
        }
    }
}
