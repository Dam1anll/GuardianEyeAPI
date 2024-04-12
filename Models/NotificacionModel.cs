using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TimeZoneConverter;

namespace GuardianEyeAPI.Models
{
    public class NotificacionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [BsonElement("Mensaje")]
        public string Mensaje { get; set; } = string.Empty;

        [BsonElement("Imagen")]
        public string Imagen { get; set; } = string.Empty;

        [BsonElement("Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [BsonElement("Video")]
        public string Video { get; set; } = string.Empty;

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = string.Empty;

    }
}
