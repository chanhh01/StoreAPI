using MongoDB.Driver;

namespace StoreModelandRepo
{
    public interface IDBClient
    {
        IMongoCollection<Store> GetStoreCollection();
    }
}
