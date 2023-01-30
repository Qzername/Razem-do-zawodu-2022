namespace CalendarioApp.Model.Server.Calendar
{
    public struct Week
    {
        public Day[] Days;

        public Week(Day[] Days)
        {
            this.Days = Days;
        }
    }
}
