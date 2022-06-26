using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenFoodFacts.Models.Products
{
    public class Food
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }
        public enum status { draft, published, trash }

        [BsonElement("status")]
        public status Status { get; set; }

        [BsonElement("imported_t")]
        public DateTime Imported_t { get; set; }

        [BsonElement("url")]
        public string? Url { get; set; }

        [BsonElement("creator")]
        public string? Creator { get; set; }

        [BsonElement("created_t")]
        public string? Created_t { get; set; }

        [BsonElement("last_modified_t")]
        public string? Last_modified_t { get; set; }

        [BsonElement("product_name")]
        public string? product_name { get; set; }

        [BsonElement("quantity")]
        public string? Quantity { get; set; }

        [BsonElement("brands")]
        public string? Brands { get; set; }

        [BsonElement("categories")]
        public string? Categories { get; set; }

        [BsonElement("labels")]
        public string? Labels { get; set; }

        [BsonElement("cities")]
        public string? Cities { get; set; }

        [BsonElement("purchase_places")]
        public string? Purchase_places { get; set; }

        [BsonElement("stores")]
        public string? Stores { get; set; }

        [BsonElement("ingredients_text")]
        public string? Ingredients_text { get; set; }

        [BsonElement("traces")]
        public string? Traces { get; set; }

        [BsonElement("serving_size")]
        public string? Serving_size { get; set; }

        [BsonElement("serving_quantity")]
        public string? Serving_quantity { get; set; }

        [BsonElement("nutriscore_score")]
        public string? Nutriscore_score { get; set; }

        [BsonElement("nutriscore_grade")]
        public string? Nutriscore_grade { get; set; }

        [BsonElement("main_category")]
        public string? main_category { get; set; }
        public string? image_url { get; set; }
       
    }


}
