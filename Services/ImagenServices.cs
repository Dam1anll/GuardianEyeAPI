using GuardianEyeAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GuardianEyeAPI.Configuration;
using MongoDB.Bson;

namespace GuardianEyeAPI.Services
{
    public class ImagenServices
    {
        private readonly IMongoCollection<ImagenModel> _imagenCollection;

        public ImagenServices(IOptions<DataBaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _imagenCollection = mongoDB.GetCollection<ImagenModel>(databaseSettings.Value.ImagenCollectionName);
        }

        public async Task<List<ImagenModel>> GetAsync() => await _imagenCollection.Find(_ => true).ToListAsync();

        public async Task<ImagenModel> GetImagenById(string Id)
        {
            return await _imagenCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertImagen(ImagenModel imagen)
        {
            await _imagenCollection.InsertOneAsync(imagen);
        }

        public async Task UpdateImagen(ImagenModel imagen)
        {
            var filter = Builders<ImagenModel>.Filter.Eq(s => s.Id, imagen.Id);
            await _imagenCollection.ReplaceOneAsync(filter, imagen);
        }

        public async Task DeleteImagen(string Id)
        {
            var filter = Builders<ImagenModel>.Filter.Eq(s => s.Id, Id);
            await _imagenCollection.DeleteOneAsync(filter);
        }
    }
}
