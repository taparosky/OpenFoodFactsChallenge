namespace OpenFoodFacts.Models
{
    public class OpenFoodFactsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string FoodsCollectionName { get; set; } = null!;

        public string CronCollectionName { get; set; } = null!;
    }
}
