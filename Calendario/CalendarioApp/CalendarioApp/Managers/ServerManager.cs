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

namespace CalendarioApp.Managers
{
    public class ServerManager
    {
        private static readonly Color EventIndicatorSelectedColor = ColorManager.GetPrimaryColor();
        private static readonly string ServerIP = "54.37.204.140";
        public static HttpClient Client = new HttpClient();
        public static EventCollection Events = new EventCollection();
        public static Token Token;

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
                        dayEvent.Add(new AdvancedEventModel { Name = task.Name, Description = task.Description, Starting = scheduleDate, ScheduleID = schedule.ID, TaskID = schedule.TaskID });
                    }

                    catch
                    {
                        Events[scheduleDate] = new DayEventCollection<AdvancedEventModel>(Color.Orange, EventIndicatorSelectedColor);
                        var dayEvent = Events[scheduleDate] as DayEventCollection<AdvancedEventModel>;
                        dayEvent.Add(new AdvancedEventModel { Name = task.Name, Description = task.Description, Starting = scheduleDate, ScheduleID = schedule.ID, TaskID = schedule.TaskID });
                        // throw new Exception($"Name: {task.Name}\nDate: {schedule.DateBegin}\nID: {schedule.ID}\nTaskID: {schedule.TaskID}");
                        // await Client.DeleteAsync($"http://{ServerIP}:6969/api/Schedule/{task.ID}");
                    }
                }
            }
        }

        // This method only adds schedule for now -> Work In Progress
        public static async System.Threading.Tasks.Task AddTaskAndSchedule(TaskCreation task, ScheduleCreation schedule)
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

        /* public static async System.Threading.Tasks.Task RemoveTask(Task task)
        {

        } */
    }
}