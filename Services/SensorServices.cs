using GuardianEyeAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GuardianEyeAPI.Configuration;
using MongoDB.Bson;

namespace GuardianEyeAPI.Services
{
    public class SensorServices
    {
        private readonly IMongoCollection<SensorModel> _usuarioCollection;

        public SensorServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usuarioCollection = mongoDB.GetCollection<SensorModel>(databaseSettings.Value.SensorCollectionName);
        }

        public async Task<List<SensorModel>> GetAsync() => await _usuarioCollection.Find(_ => true).ToListAsync();

        public async Task<SensorModel> GetDriverById(string Id)
        {
            return await _usuarioCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertDriver(SensorModel sensor)
        {
            await _usuarioCollection.InsertOneAsync(sensor);
        }

        public async Task UpdateDriver(SensorModel sensor)
        {
            var filter = Builders<SensorModel>.Filter.Eq(s => s.Id, sensor.Id);
            await _usuarioCollection.ReplaceOneAsync(filter, sensor);
        }

        public async Task DeleteDriver(string Id)
        {
            var filter = Builders<SensorModel>.Filter.Eq(s => s.Id, Id);
            await _usuarioCollection.DeleteOneAsync(filter);
        }
    }
}
