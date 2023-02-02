using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using CalendarioApp.Model.Server;
using System.Text.Json;
using System.Net.Http.Headers;
using Xamarin.Plugin.Calendar.Models;
using CalendarioApp.Model.App;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace CalendarioApp.Managers
{
    public class ServerManager
    {
        private static readonly Color EventIndicatorSelectedColor = ColorManager.GetPrimaryColor();
        private static readonly string ServerIP = "20.25.191.186"; // "54.37.204.140";
        public static HttpClient Client = new HttpClient();
        public static EventCollection Events = new EventCollection();
        private static Token Token;

        public static async System.Threading.Tasks.Task<Token> Login(AccountCredentials accountCredentials)
        {
            string accountCredentialsDict = JsonSerializer.Serialize(accountCredentials);

            StringContent content = new StringContent(accountCredentialsDict, System.Text.Encoding.UTF8);
            MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType = headerValue;

            var response = await Client.PostAsync($"http://{ServerIP}:6969/api/Account/Login", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed login");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            Token = JSONManager.Deserialize<Token>(responseString);
            Client.DefaultRequestHeaders.Add("token", Token.Package);
            return Token;
        }

        public static void ClearEvents() { Events.Clear();}

        public static async System.Threading.Tasks.Task Setup()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Task");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed tasks");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            Task[] tasks = JSONManager.Deserialize<Task[]>(responseString);
            foreach (Task task in tasks)
            {
                var scheduleResponse = await Client.GetAsync($"http://{ServerIP}:6969/api/Schedule/{task.ID}");
                string scheduleResponseString = await scheduleResponse.Content.ReadAsStringAsync();

                if (scheduleResponseString == null)
                {
                    throw new Exception("Failed schedules");
                }

                Schedule[] schedules = JSONManager.Deserialize<Schedule[]>(scheduleResponseString);
                foreach (Schedule schedule in schedules)
                {
                    DateTime scheduleBegin = new DateTime(schedule.DateBegin);
                    DateTime scheduleEnd = new DateTime(schedule.DateEnd);

                    try
                    {
                        var dayEvent = Events[scheduleBegin] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel(task.Name, task.Description, scheduleBegin, scheduleEnd, schedule.ID, schedule.TaskID, schedule.PriorityID));
                    }

                    catch
                    {
                        Events[scheduleBegin] = new DayEventCollection<AdvancedEventModel>(Color.FromHex("#0080ff"), EventIndicatorSelectedColor);
                        var dayEvent = Events[scheduleBegin] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel(task.Name, task.Description, scheduleBegin, scheduleEnd, schedule.ID, schedule.TaskID, schedule.PriorityID));
                    }
                }
            }
        }

        public static async System.Threading.Tasks.Task<Task[]> GetTasks()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Task");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed tasks");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            Task[] tasks = JSONManager.Deserialize<Task[]>(responseString);
            return tasks;
        }

        public static async System.Threading.Tasks.Task AddTask(TaskCreation task)
        {
            string scheduleDict = JsonSerializer.Serialize(task);

            StringContent content = new StringContent(scheduleDict, System.Text.Encoding.UTF8);
            MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType = headerValue;

            var response = await Client.PostAsync($"http://{ServerIP}:6969/api/Task", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed add task");
            }
        }

        public static async System.Threading.Tasks.Task RemoveTask(Task task)
        {
            await Client.DeleteAsync($"http://{ServerIP}:6969/api/Task/{task.ID}");
        }

        public static async System.Threading.Tasks.Task<Schedule[]> GetSchedulesUsingTasks(Task[] tasks)
        {
            Schedule[] schedules = { };

            foreach (Task task in tasks)
            {
                var scheduleResponse = await Client.GetAsync($"http://{ServerIP}:6969/api/Schedule/{task.ID}");
                string scheduleResponseString = await scheduleResponse.Content.ReadAsStringAsync();

                if (scheduleResponseString == null)
                {
                    throw new Exception("Failed schedules");
                }

                Schedule[] _schedules = JSONManager.Deserialize<Schedule[]>(scheduleResponseString);
                foreach (Schedule _schedule in _schedules)
                {
                    schedules.Append(_schedule);
                }
            }

            return schedules;
        }

        public static async System.Threading.Tasks.Task AddSchedule(ScheduleCreation schedule)
        {
            string scheduleDict = JsonSerializer.Serialize(schedule);

            StringContent content = new StringContent(scheduleDict, System.Text.Encoding.UTF8);
            MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType = headerValue;

            var response = await Client.PostAsync($"http://{ServerIP}:6969/api/Schedule", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed add schedule");
            }
        }

        public static async System.Threading.Tasks.Task RemoveSchedule(AdvancedEventModel schedule)
        {
            await Client.DeleteAsync($"http://{ServerIP}:6969/api/Schedule/{schedule.ScheduleID}");
        }

        public static async System.Threading.Tasks.Task<Priority[]> GetPriorities()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Priority");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed priorities");
            }
            string responseString = await response.Content.ReadAsStringAsync();
            Priority[] priorities = JSONManager.Deserialize<Priority[]>(responseString);

            return priorities;
        }

        public static async System.Threading.Tasks.Task AddPriority(PriorityCreation priority)
        {
            string priorityDict = JsonSerializer.Serialize(priority);

            StringContent content = new StringContent(priorityDict, System.Text.Encoding.UTF8);
            MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentType = headerValue;

            var response = await Client.PostAsync($"http://{ServerIP}:6969/api/Priority", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed add priority");
            }
        }

        public static async System.Threading.Tasks.Task<Task[]> GetDay(Date date)
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Calendar/Day/{date.Day}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed day");
            }
            string responseString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);
            Task[] tasks = JSONManager.Deserialize<Task[]>(json["Tasks"].ToString());

            return tasks;
        }
    }
}