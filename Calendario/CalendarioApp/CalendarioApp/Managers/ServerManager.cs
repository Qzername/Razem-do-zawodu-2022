using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CalendarioApp.Model.Server;
using System.Text.Json;
using System.Net.Http.Headers;
using Xamarin.Plugin.Calendar.Models;
using CalendarioApp.Model.App;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using CalendarioApp.Model.Server.Calendar;
using Newtonsoft.Json.Linq;
using System.Text;

namespace CalendarioApp.Managers
{
    public class ServerManager
    {
        private static readonly Color EventIndicatorSelectedColor = ColorManager.GetPrimaryColor();
        // private static readonly string ServerIP = "54.37.204.140";
        private static readonly string ServerIP = "192.168.0.103";
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
                throw new Exception("Failed");
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
                throw new Exception("Failed");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            Task[] tasks = JSONManager.Deserialize<Task[]>(responseString);
            foreach (Task task in tasks)
            {
                var scheduleResponse = await Client.GetAsync($"http://{ServerIP}:6969/api/Schedule/{task.ID}");
                string scheduleResponseString = await scheduleResponse.Content.ReadAsStringAsync();

                if (scheduleResponseString == null)
                {
                    throw new Exception("Failed");
                }

                Schedule[] schedules = JSONManager.Deserialize<Schedule[]>(scheduleResponseString);
                foreach (Schedule schedule in schedules)
                {
                    DateTime scheduleDate = new DateTime(schedule.DateBegin);
                    try
                    {
                        var dayEvent = Events[scheduleDate] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel { Name = task.Name, Description = task.Description, Starting = scheduleDate, ScheduleID = schedule.ID, TaskID = schedule.TaskID, PriorityID = schedule.PriorityID });
                    }

                    catch
                    {
                        Events[scheduleDate] = new DayEventCollection<AdvancedEventModel>(Color.FromHex("#0080ff"), EventIndicatorSelectedColor);
                        var dayEvent = Events[scheduleDate] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel { Name = task.Name, Description = task.Description, Starting = scheduleDate, ScheduleID = schedule.ID, TaskID = schedule.TaskID, PriorityID = schedule.PriorityID });
                    }
                }
            }
        }

        public static async System.Threading.Tasks.Task<Task[]> GetTasks()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Task");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed");
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
                throw new Exception("Failed");
            }
        }

        /* public static async System.Threading.Tasks.Task RemoveTask(Task task)
        {

        } */

        public static async System.Threading.Tasks.Task<Schedule[]> GetSchedulesUsingTasks(Task[] tasks)
        {
            Schedule[] schedules = { };

            foreach (Task task in tasks)
            {
                var scheduleResponse = await Client.GetAsync($"http://{ServerIP}:6969/api/Schedule/{task.ID}");
                string scheduleResponseString = await scheduleResponse.Content.ReadAsStringAsync();

                if (scheduleResponseString == null)
                {
                    throw new Exception("Failed");
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
                throw new Exception("Failed");
            }
        }

        public static async System.Threading.Tasks.Task<Priority[]> GetPriorities()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Priority");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed");
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
                throw new Exception("Failed");
            }
        }

        public static async System.Threading.Tasks.Task<Task[]> GetDay(Date date)
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Calendar/Day/{date.Day}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string responseString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);
            Task[] tasks = JSONManager.Deserialize<Task[]>(json["Tasks"].ToString());

            return tasks;
        }
    }
}