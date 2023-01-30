namespace CalendarioApp.Model.Server
{
    public struct TokenData
    {
        public int ID { get; set; }
        public string Login { get; set; }

        public TokenData(int ID, string Login)
        {
            this.ID = ID;
            this.Login = Login;
        }
    }
}
