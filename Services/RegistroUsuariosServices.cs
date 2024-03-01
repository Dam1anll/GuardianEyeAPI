using GuardianEyeAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GuardianEyeAPI.Configuration;
using MongoDB.Bson;

namespace GuardianEyeAPI.Services
{
    public class RegistroUsuariosServices
    {
        private readonly IMongoCollection<RegistroUsuariosModel> _registroUsuariosCollection;

        public RegistroUsuariosServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _registroUsuariosCollection = mongoDB.GetCollection<RegistroUsuariosModel>(databaseSettings.Value.RegistrarCollectionName);
        }

        public async Task<List<RegistroUsuariosModel>> GetAsync() => await _registroUsuariosCollection.Find(_ => true).ToListAsync();

        public async Task<RegistroUsuariosModel> GetDriverById(string Id)
        {
            return await _registroUsuariosCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertDriver(RegistroUsuariosModel notificacion)
        {
            await _registroUsuariosCollection.InsertOneAsync(notificacion);
        }

        public async Task UpdateDriver(RegistroUsuariosModel notificacion)
        {
            var filter = Builders<RegistroUsuariosModel>.Filter.Eq(s => s.Id, notificacion.Id);
            await _registroUsuariosCollection.ReplaceOneAsync(filter, notificacion);
        }

        public async Task DeleteDriver(string Id)
        {
            var filter = Builders<RegistroUsuariosModel>.Filter.Eq(s => s.Id, Id);
            await _registroUsuariosCollection.DeleteOneAsync(filter);
        }
    }
}
