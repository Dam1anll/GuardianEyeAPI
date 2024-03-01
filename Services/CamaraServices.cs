using GuardianEyeAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GuardianEyeAPI.Configuration;
using MongoDB.Bson;
namespace GuardianEyeAPI.Services
{
    public class CamaraServices
    {
        private readonly IMongoCollection<CamaraModel> _camaraCollection;

        public CamaraServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _camaraCollection = mongoDB.GetCollection<CamaraModel>(databaseSettings.Value.CamaraCollectionName);
        }

        public async Task<List<CamaraModel>> GetAsync() => await _camaraCollection.Find(_ => true).ToListAsync();

        public async Task<CamaraModel> GetCamaraById(string Id)
        {
            return await _camaraCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertCamara(CamaraModel camara)
        {
            await _camaraCollection.InsertOneAsync(camara);
        }

        public async Task UpdateCamara(CamaraModel camara)
        {
            var filter = Builders<CamaraModel>.Filter.Eq(s => s.Id, camara.Id);
            await _camaraCollection.ReplaceOneAsync(filter, camara);
        }

        public async Task DeleteCamara(string Id)
        {
            var filter = Builders<CamaraModel>.Filter.Eq(s => s.Id, Id);
            await _camaraCollection.DeleteOneAsync(filter);
        }
    }
}
