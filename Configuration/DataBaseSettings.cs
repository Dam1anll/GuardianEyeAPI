namespace GuardianEyeAPI.Configuration
{
    public class DataBaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
        public string CamaraCollectionName { get; set; } = string.Empty;
        public string NotiCollectionName { get; set; } = string.Empty;
        public string SensorCollectionName { get; set; } = string.Empty;
        public string RegistrarCollectionName { get; set; } = string.Empty;
        public string ImagenCollectionName { get; set; } = string.Empty;
    }
}
