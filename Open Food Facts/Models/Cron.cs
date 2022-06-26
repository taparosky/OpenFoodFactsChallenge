using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenFoodFacts.Models
{
    public class Cron
    {
        public ObjectId Id { get; set; }
        public string? DbReadWriteConnection { get; set; }
        public DateTime LastUpdate_t { get; set; }
        public TimeSpan OnlineTime_t { get; set; }
        public long? UsedMemory { get; set; }
        public string FileUploaded { get; set; }



    }
}
