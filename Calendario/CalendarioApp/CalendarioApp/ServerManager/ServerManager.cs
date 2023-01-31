using System;
using System.Net;
using System.Net.Http;
using CalendarioApp.Model.Server;
using System.Text.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net.Mime;
using System.Text;

namespace CalendarioApp.ServerManager
{
    public class ServerManager
    {
        private static readonly string ServerIP = "54.37.204.140";
        public static HttpClient Client = new HttpClient();
        public static Token Token;

        public static async System.Threading.Tasks.Task<Token> Login(AccountCredentials accountCredentials)
        {
            string accountCredentialsDict = JsonSerializer.Serialize(accountCredentials);

            StringContent content = new StringContent(accountCredentialsDict, System.Text.Encoding.UTF8);
            MediaTypeHeaderValue mValue = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType = mValue;

            var response = await Client.PostAsync($"http://{ServerIP}:6969/api/Account/Login", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(responseString);
            long expiration = json["expiration"].ToObject<long>();
            string package = json["package"].ToObject<string>();

            Token = new Token(expiration, package);
            Client.DefaultRequestHeaders.Add("token", Token.Package);
            return Token;
        }

        public static async System.Threading.Tasks.Task GetTasks()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Task");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            Task[] tasks = JSONManager.Deserialize<Task[]>(responseString);
            foreach (Task task in tasks)
            {
                var scheduleResponse = await Client.GetAsync($"http://{ServerIP}:6969/api/Task");
            }
        }

        public static async System.Threading.Tasks.Task AddTask(TaskCreation task)
        {

        }
    }
}