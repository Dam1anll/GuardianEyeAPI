using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GuardianEyeAPI.Models
{
    public class SensorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id = string.Empty;

        [BsonElement("Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [BsonElement("Estado")]
        public bool Estado { get; set; } 
    }
}
