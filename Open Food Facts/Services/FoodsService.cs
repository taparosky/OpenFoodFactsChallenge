using MongoDB.Driver;
using Microsoft.Extensions.Options;
using OpenFoodFacts.Models;
using OpenFoodFacts.Models.Products;
using static OpenFoodFacts.Models.Products.Food;
using OpenFoodFacts.Models.Wrappers;
using Microsoft.AspNetCore.Mvc;
using OpenFoodFacts.Wrappers;
using System.Text.Json;
using OpenFoodFacts.Services.Interfaces;
using OpenFoodFacts.Models.Helpers;
using MongoDB.Bson;

namespace OpenFoodFacts.Services
{
    public class FoodsService
    {
        private readonly IMongoCollection<Food> _foodsCollection;
        private readonly IUriService _uriService;
        public FoodsService(IOptions<OpenFoodFactsDatabaseSettings> openFoodFactsDatabaseSettings, IUriService UriService)
        {
            var mongoClient = new MongoClient(openFoodFactsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(openFoodFactsDatabaseSettings.Value.DatabaseName);

            _foodsCollection = mongoDatabase.GetCollection<Food>(openFoodFactsDatabaseSettings.Value.FoodsCollectionName);

            _uriService = UriService;
        }

        public async Task<PagedResponse<List<Food>>> GetAsync([FromQuery] PaginationFilter filter, string route)
        {
            var pagedData = await _foodsCollection
                .Find(_ => true)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Limit(filter.PageSize)
                .ToListAsync();
            var totalRecords = await _foodsCollection.CountDocumentsAsync(new BsonDocument());
            var pagedResponse = PaginationHelper.CreatePagedReponse<Food>(pagedData,filter, ((int)totalRecords), _uriService, route);
            return pagedResponse;


        }
            

        public async Task<Food?> GetAsync(string code) =>
            await _foodsCollection.Find(x => x.code == code).FirstOrDefaultAsync();

        public async Task CreateAsync(Food newFood) =>
            await _foodsCollection.InsertOneAsync(newFood);

        public async Task UpdateAsync(string code, Food updatedFood) =>
            await _foodsCollection.ReplaceOneAsync(x => x.code == code, updatedFood);

        public async Task Delete(string code)
        {
            var update = Builders<Food>.Update.Set(f => f.Status, Food.status.trash);
            await _foodsCollection.UpdateOneAsync(f => f.code == code, update);
        }

    }
}
