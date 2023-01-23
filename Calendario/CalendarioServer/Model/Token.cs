namespace CalendarioAPI.Model
{
    public struct Token
    {
        public long Expiration { get; set; }
        public string Package { get; set; }

        public Token(long Expiration, string Package)
        {
            this.Expiration = Expiration;
            this.Package = Package; 
        }
    }
}
