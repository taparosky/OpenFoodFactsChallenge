using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenFoodFacts.Models.Products
{
    public class Food
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        public string code { get; set; }
        public enum status { draft, published, trash }

        [BsonElement("status")]
        public status Status { get; set; }


        public DateTime imported_t { get; set; }


        public string? url { get; set; }

        public string? creator { get; set; }

        public string? created_t { get; set; }

        public string? last_modified_t { get; set; }

        public string? product_name { get; set; }

        public string? quantity { get; set; }


        public string? brands { get; set; }


        public string? categories { get; set; }


        public string? labels { get; set; }


        public string? cities { get; set; }

        public string? purchase_places { get; set; }
        public string? stores { get; set; }

        public string? ingredients_text { get; set; }

        public string? traces { get; set; }

        public string? serving_size { get; set; }

        public string? serving_quantity { get; set; }

        public string? nutriscore_score { get; set; }

        public string? nutriscore_grade { get; set; }

        public string? main_category { get; set; }
        public string? image_url { get; set; }
       
    }


}
