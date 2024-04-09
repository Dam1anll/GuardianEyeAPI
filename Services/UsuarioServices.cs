using GuardianEyeAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GuardianEyeAPI.Configuration;
using MongoDB.Bson;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GuardianEyeAPI.Services
{
    public class UsuarioServices
    {
        private readonly IMongoCollection<UsuarioModel> _usuarioCollection;

        public UsuarioServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usuarioCollection = mongoDB.GetCollection<UsuarioModel>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<UsuarioModel>> GetAsync() => await _usuarioCollection.Find(_ => true).ToListAsync();

        public async Task<UsuarioModel> GetDriverById(string Id)
        {
            return await _usuarioCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertDriver(UsuarioModel usuario)
        {
            await _usuarioCollection.InsertOneAsync(usuario);
        }

        public async Task UpdateDriver(UsuarioModel usuario)
        {
            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, usuario.Id);
            await _usuarioCollection.ReplaceOneAsync(filter, usuario);
        }

        public async Task DeleteDriver(string Id)
        {
            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, Id);
            await _usuarioCollection.DeleteOneAsync(filter);
        }

        public async Task<UsuarioModel> GetByEmailAndPassword(string correo, string contraseña)
        {
            // Realizar la lógica para buscar el usuario por correo y contraseña
            return await _usuarioCollection.Find(u => u.Correo == correo && u.Contraseña == contraseña).FirstOrDefaultAsync();
        }
    }
}
