namespace CalendarioAPI.Model
{
    public struct Priority
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string ColorHex { get; set; }
    }

    public struct PriorityCreation
    {
        public string Name { get; set; }
        public string ColorHex { get; set; }
    }
}
