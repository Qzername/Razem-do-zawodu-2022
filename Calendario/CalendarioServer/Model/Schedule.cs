namespace CalendarioAPI.Model
{
    public struct Schedule
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        /// <summary>
        /// -1 = nie ustawione
        /// </summary>
        public int PriorityID { get; set; }
        public long DateBegin { get; set; }
        public long DateEnd { get; set; }
        public long DateRemind { get; set; }
    }

    public struct ScheduleCreation
    {
        public int TaskID { get; set; }
        public long DateBegin { get; set; }
        public long? DateEnd { get; set; }
        public long? DateRemind { get; set; }
    }
}
