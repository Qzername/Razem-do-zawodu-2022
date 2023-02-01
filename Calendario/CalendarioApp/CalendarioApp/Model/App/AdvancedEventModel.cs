using System;

namespace CalendarioApp.Model.App
{
    public class AdvancedEventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Starting { get; set; }
        public int ScheduleID { get; set; }
        public int TaskID { get; set; }
        public int PriorityID { get; set; }
    }
}
