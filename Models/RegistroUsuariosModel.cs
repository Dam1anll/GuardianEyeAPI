using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TimeZoneConverter;
namespace GuardianEyeAPI.Models
{
    public class RegistroUsuariosModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id = string.Empty;

        [BsonElement("Correo")]
        public string Correo { get; set; } = string.Empty;

        [BsonElement("Contraseña")]
        public string Contraseña { get; set; } = string.Empty;

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

       
    }
}
