namespace CalendarioAPI.Model
{
    public struct Schedule
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string DateBegin { get; set; }
        public string DateEnd { get; set; }
    }
}
