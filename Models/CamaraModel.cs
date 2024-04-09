using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace GuardianEyeAPI.Models
{
    public class CamaraModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id = string.Empty;

        [BsonElement("Ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;

        [BsonElement("Estado")]
        public string Estado { get; set; } = string.Empty; 

        [BsonElement("Modelo")]
        public string Modelo { get; set; } = string.Empty;
    }
}
