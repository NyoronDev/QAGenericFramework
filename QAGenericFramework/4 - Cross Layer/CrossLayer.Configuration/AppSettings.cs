namespace CrossLayer.Configuration
{
    public class AppSettings
    {
        public ExecutionType ExecutionType { get; set; }
        public AppConfiguration AppConfiguration { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public SauceLabs SauceLabs { get; set; }
    }
}
