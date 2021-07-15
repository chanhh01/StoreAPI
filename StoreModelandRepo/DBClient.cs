using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace StoreModelandRepo
{
    public class DBClient : IDBClient
    {
        private readonly IMongoCollection<Store> _store;
        public DBClient(IOptions<StoreDBConfig> storedbconfig)
        {
            string ConnectionString = "mongodb://chan:chh428493@testchanvm.eastasia.cloudapp.azure.com?maxPoolSize=500";
            string Database_Name = "newTest";
            string Store_Collection_Name = "TestAPI";

            
            MongoClient client = new MongoClient(ConnectionString);
            IMongoDatabase database = client.GetDatabase(Database_Name);
            _store = database.GetCollection<Store>(Store_Collection_Name);

            // The code below will commuicate with storedbconfig which should retrieve connection string from appsettings.json,
            // database name and collection name from launchsettings.json. This code would only work if you run the code using IIS
            // instead of running it in debug file since the code in debug file would not refer to launchsettings and appsettings for some reason.

            //MongoClient client = new MongoClient(storedbconfig.Value.ConnectionString);
            //IMongoDatabase database = client.GetDatabase(storedbconfig.Value.Database_Name);
            //_store = database.GetCollection<Store>(storedbconfig.Value.Store_Collection_Name);
        }

        public IMongoCollection<Store> GetStoreCollection()
        {
            return _store;
        }
    }
}
