namespace CalendarioApp.Model.Server
{
    public struct Task
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        /// <summary>
        /// -1 = nie ustawione
        /// </summary>
        public int PriorityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconID { get; set; }
        public bool IsCompleted { get; set; }
    }

    public struct TaskCreation
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
