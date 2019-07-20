namespace BilgeAdam.Sinemaskop
{
    public class AppSettings
    {
        public Info Author { get; set; }
        public ConnectionString ConnectionStrings { get; set; }

        public class ConnectionString
        {
            public string SinContext { get; set; }
        }

        public class Info
        {
            public string Name { get; set; }
            public string Phone { get; set; }
        }
    }
}
