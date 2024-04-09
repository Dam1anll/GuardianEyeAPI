using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TimeZoneConverter;
namespace GuardianEyeAPI.Models
{
    public class ImagenModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [BsonElement("Tamaño")]
        public int Tamaño { get; set; }

        [BsonElement("Camara")]
        public string Camara { get; set; } = string.Empty;

 

    }
}
