using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuardianEyeAPI.Models
{
    public class UsuarioModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("ApellidoP")]
        public string ApellidoP { get; set;} = string.Empty;

        [BsonElement("ApellidoM")]
        public string ApellidoM { get; set; } = string.Empty;

        [BsonElement("Correo")]
        public string Correo { get; set; } = string.Empty;

        [BsonElement("Contraseña")]
        public string Contraseña { get; set;} = string.Empty;

        [BsonElement("NumeroCelular")]
        public Int64 NumeroCelular { get; set;} 

    }
    public class LoginModel
    {
        [BsonElement("Correo")]
        public string Correo { get; set; }

        [BsonElement("Contraseña")]
        public string Contraseña { get; set; }
    }
}
