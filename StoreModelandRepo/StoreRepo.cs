using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreModelandRepo
{
    public class StoreRepo : IStoreRepo
    {
        public IMongoCollection<Store> _store;
        public StoreRepo(IDBClient dbclient)
        {
            _store = dbclient.GetStoreCollection();
        }

        public async Task<bool> AddStore(Store store)
        {
            await _store.InsertOneAsync(store);
            return true;
        }

        public async Task DeleteStore(string id)
        {
            await _store.DeleteOneAsync(store => store.id == id);
        }

        public async Task<List<Store>> GetStore()
        {
            List<Store> store = (await _store.FindAsync(store => true)).ToList();
            return store;
        }

        public async Task<Store> GetStore(string id)
        {
            Store store = (await _store.FindAsync(store => store.id == id)).First();
            return store;
        }

        public async Task<Store> UpdateStore(string id, Store store)
        {
            await _store.ReplaceOneAsync(s => s.id == store.id, store);
            return store;
        }
    }
}
