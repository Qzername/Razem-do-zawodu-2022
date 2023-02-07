using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<Task> Tasks = new ObservableCollection<Task>();
        public static EventCollection Events = new EventCollection();
        public static ObservableCollection<Priority> Priorities = new ObservableCollection<Priority>();
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

        public static async System.Threading.Tasks.Task Sync()
        {
            Tasks.Clear();
            Priorities.Clear();
            Events.Clear();
            NotificationManager.CancelAll();

            await GetTasks();
            await GetPriorities();
            await GetSchedules(Tasks);
        }



        public static async System.Threading.Tasks.Task GetTasks()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Task");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed tasks");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            foreach (Task x in JSONManager.Deserialize<Task[]>(responseString))
            {
                Tasks.Add(x);
            };
        }

        public static async System.Threading.Tasks.Task AddTask(TaskCreation task)
        {
            string taskDict = JsonSerializer.Serialize(task);

            StringContent content = new StringContent(taskDict, System.Text.Encoding.UTF8);
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



        public static async System.Threading.Tasks.Task GetSchedules(ObservableCollection<Task> tasks)
        {
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
                    string colorHex = "#ffffff";

                    try
                    { 
                        colorHex = Priorities.Single(x => x.ID == schedule.PriorityID).ColorHex;
                    }

                    catch { }

                    try
                    {
                        var dayEvent = Events[scheduleBegin] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel(task.Name, task.Description, scheduleBegin, scheduleEnd, schedule.ID, task.ID, schedule.PriorityID, colorHex));
                        await NotificationManager.Schedule(task.Name, task.Description, scheduleBegin, scheduleEnd);
                    }

                    catch
                    {
                        Events[scheduleBegin] = new DayEventCollection<AdvancedEventModel>(Color.FromHex("#0080ff"), EventIndicatorSelectedColor);
                        var dayEvent = Events[scheduleBegin] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel(task.Name, task.Description, scheduleBegin, scheduleEnd, schedule.ID, task.ID, schedule.PriorityID, colorHex));
                        await NotificationManager.Schedule(task.Name, task.Description, scheduleBegin, scheduleEnd);
                    }
                }
            }
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



        public static async System.Threading.Tasks.Task GetPriorities()
        {
            var response = await Client.GetAsync($"http://{ServerIP}:6969/api/Priority");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed priorities");
            }
            string responseString = await response.Content.ReadAsStringAsync();

            foreach (Priority x in JSONManager.Deserialize<Priority[]>(responseString))
            {
                Priorities.Add(x);
            };
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

        public static async System.Threading.Tasks.Task RemovePriority(Priority priority)
        {
            await Client.DeleteAsync($"http://{ServerIP}:6969/api/Priority/{priority.ID}");
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