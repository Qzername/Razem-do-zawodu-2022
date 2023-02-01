using CalendarioAPI.Model;
using ForesterAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromHeader] string token)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var priority = SQLDatabase.Select<Priority>($"SELECT * FROM Priorities WHERE AccountID={loginToken.ID}");

            return Ok(JSONManager.Serialize(priority));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string token, int id)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var priority = SQLDatabase.Select<Priority>($"SELECT * FROM Priorities WHERE AccountID={loginToken.ID} AND ID={id}");

            if (priority.Length == 0)
                return StatusCode(404);

            return Ok(JSONManager.Serialize(priority[0]));
        }

        [HttpPost]
        public IActionResult Post([FromHeader] string token, PriorityCreation priority)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            if (string.IsNullOrEmpty(priority.Name))
                return StatusCode(400);

            SQLDatabase.NoReturnQuery($"INSERT INTO Priorities(AccountID, Name, ColorHex) VALUES({loginToken.ID}, \"{priority.Name}\", \"{priority.ColorHex}\")");

            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch([FromHeader] string token, Priority priority)
        {
            string decoded = JWTManager.Decode(token);

            if (decoded == "")
                return StatusCode(403);

            var loginToken = JSONManager.Deserialize<TokenData>(decoded);

            var tasks = SQLDatabase.Select<Priority>($"SELECT * FROM Priorities WHERE AccountID={loginToken.ID} AND ID={priority.ID}");

            if (tasks.Length == 0)
                return StatusCode(404);

            string query = "UPDATE Priorities SET ";
            
            if (!string.IsNullOrEmpty(priority.Name))
                query += $"Name=\"{priority.Name}\",";

            if (!string.IsNullOrEmpty(priority.ColorHex))
                query += $"ColorHex=\"{priority.ColorHex}\",";

            if (query[^1] != ',')
                return StatusCode(406);

            query = query.Remove(query.Length - 1);
            query += $" WHERE ID={priority.ID}";

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

            var priorities = SQLDatabase.Select<Priority>($"SELECT * FROM Priorities WHERE AccountID=\"{loginToken.ID}\" AND ID={id}");

            if (priorities.Length == 0)
                return StatusCode(404);

            SQLDatabase.NoReturnQuery($"DELETE FROM Priorities WHERE ID={id}");

            return Ok();
        }   
    }
}
