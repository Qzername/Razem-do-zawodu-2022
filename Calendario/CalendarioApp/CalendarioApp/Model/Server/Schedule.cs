namespace CalendarioApp.Model.Server
{
    public struct Schedule
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public long DateBegin { get; set; }
        public long DateEnd { get; set; }
    }

    public struct ScheduleCreation
    {
        public int TaskID { get; set; }
        public long DateBegin { get; set; }
        public long? DateEnd { get; set; }
    }
}
