using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OpenFoodFacts.Models;

namespace OpenFoodFacts.Services
{
    public class CronService
    {
        private readonly IMongoCollection<Cron> _cronCollection;

        public CronService(IOptions<OpenFoodFactsDatabaseSettings> openFoodFactsDatabaseSettings)
        {
            var mongoClient = new MongoClient(openFoodFactsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(openFoodFactsDatabaseSettings.Value.DatabaseName);

            _cronCollection = mongoDatabase.GetCollection<Cron>(openFoodFactsDatabaseSettings.Value.CronCollectionName);
        }

        public async Task<Cron> GetAsync() =>
            await _cronCollection.Find<Cron>(x => true)
                                              .SortByDescending(d => d.LastUpdate_t)
                                              .Limit(1).FirstOrDefaultAsync<Cron>();


    }
}
