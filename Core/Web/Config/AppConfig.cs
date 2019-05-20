namespace Core.Web.Config
{
    public interface IAppConfig
    {
        SeedSettings SeedSettings { get; set; }
        bool RequireHttps { get; set; }
    }
    
    public class AppConfig : IAppConfig
    {
        public SeedSettings SeedSettings { get; set; }
        public bool RequireHttps { get; set; }
    }

    public class SeedSettings
    {
        public bool ShouldResetDatabase { get; set; }
    }
}