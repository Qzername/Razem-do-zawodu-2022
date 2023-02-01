using System;

namespace CalendarioApp.Model.Server
{
    public struct Date
    {
        public long Day { get; set; }

        public Date(DateTime date)
        {
            this.Day = date.Ticks;
        }
    }
}