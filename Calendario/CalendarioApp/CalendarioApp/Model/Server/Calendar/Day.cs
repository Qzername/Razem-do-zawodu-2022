namespace CalendarioApp.Model.Server.Calendar
{
    public struct Day
    {
        public Task[] Tasks { get; set; }
    
        public Day(Task[] Tasks)
        {
            this.Tasks = Tasks;
        }
    }
}
