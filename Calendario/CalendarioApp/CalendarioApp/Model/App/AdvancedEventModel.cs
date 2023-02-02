using System;

namespace CalendarioApp.Model.App
{
    public class AdvancedEventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Starting { get; set; }
        public DateTime? Ending { get; set; }
        public int ScheduleID { get; set; }
        public int TaskID { get; set; }
        public int PriorityID { get; set; }
        public string ColorHex { get; set; }

        public AdvancedEventModel(string Name, string Description, DateTime Starting, DateTime? Ending, int ScheduleID, int TaskID, int PriorityID, string ColorHex)
        {
            this.Name = Name;
            this.Description = Description;
            this.Starting = Starting;
            if (Ending != null) { this.Ending = Ending; } else { this.Ending = Starting; }
            this.ScheduleID = ScheduleID;
            this.TaskID = TaskID;
            this.PriorityID = PriorityID;
            this.ColorHex = ColorHex;
        }
    }
}
