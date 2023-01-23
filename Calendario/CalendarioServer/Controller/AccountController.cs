using CalendarioAPI.Model;
using ForesterAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalendarioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /*
         Warunki do stworzenia konta:
         - nazwa loginu jest większa od 8 i krótsza od 20 symboli
         - hasło nie zawiera spacji etc.
         - hasło jest większe od 8 i mniejsze od 50 symboli
         - hasło zawiera przynajmniej jeden znak specjalny (np. @!&)
         - hasło zawiera przynajmniej jedną cyfrę
         - hasło ma przynajmniej jeden znak duży
         - hasło ma przynajmniej jeden znak mały
         */
        static Regex loginRegex = new Regex(".{8,20}");
        static Regex passwordRegex = new Regex("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,50}");

        [HttpPost("[action]")]
        public IActionResult Register(AccountCredentials credentials)
        {
            if (!loginRegex.IsMatch(credentials.Login)) 
                return StatusCode(400);

            if (!passwordRegex.IsMatch(credentials.Password))
                return StatusCode(400);

            //Sprawdzanie czy login nie jest zajęty
            if (SQLDatabase.Select<int>($"SELECT EXISTS(SELECT * FROM Accounts WHERE Login=\"{credentials.Login}\" )")[0] > 0)
                return StatusCode(403);

            //kryptowanie hasła
            credentials.Password = Sha256(credentials.Password);

            SQLDatabase.NoReturnQuery($"INSERT INTO Accounts(Login,Password) VALUES(\"{credentials.Login}\",\"{credentials.Password}\")");

            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Login(AccountCredentials credentials)
        {
            if (!loginRegex.IsMatch(credentials.Login))
                return StatusCode(400);

            if (!passwordRegex.IsMatch(credentials.Password))
                return StatusCode(400);

            //kryptowanie hasła
            credentials.Password = Sha256(credentials.Password);

            var users = SQLDatabase.Select<Account>($"SELECT * FROM Accounts WHERE Login=\"{credentials.Login}\" AND Password=\"{credentials.Password}\"");

            if (users.Length == 0)
                return StatusCode(404);

            long expTime = DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds();

            Dictionary<string, object> claims = new Dictionary<string, object>() { { "ID", users[0].ID }, { "Login", users[0].Login } };

            return Ok(new Token(expTime, JWTManager.Encode(claims)));
        }

        string Sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
