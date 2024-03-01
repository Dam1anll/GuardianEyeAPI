using GuardianEyeAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GuardianEyeAPI.Configuration;
using MongoDB.Bson;

namespace GuardianEyeAPI.Services
{
    public class NotificacionServices
    {
        private readonly IMongoCollection<NotificacionModel> _notificacionCollection;

        public NotificacionServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _notificacionCollection = mongoDB.GetCollection<NotificacionModel>(databaseSettings.Value.NotiCollectionName);
        }

        public async Task<List<NotificacionModel>> GetAsync() => await _notificacionCollection.Find(_ => true).ToListAsync();

        public async Task<NotificacionModel> GetDriverById(string Id)
        {
            return await _notificacionCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertDriver(NotificacionModel notificacion)
        {
            await _notificacionCollection.InsertOneAsync(notificacion);
        }

        public async Task UpdateDriver(NotificacionModel notificacion)
        {
            var filter = Builders<NotificacionModel>.Filter.Eq(s => s.Id, notificacion.Id);
            await _notificacionCollection.ReplaceOneAsync(filter, notificacion);
        }

        public async Task DeleteDriver(string Id)
        {
            var filter = Builders<NotificacionModel>.Filter.Eq(s => s.Id, Id);
            await _notificacionCollection.DeleteOneAsync(filter);
        }
    }
}
