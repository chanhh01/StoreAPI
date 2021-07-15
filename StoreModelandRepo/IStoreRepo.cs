using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreModelandRepo
{
    public interface IStoreRepo
    {
        Task<List<Store>> GetStore();
        Task<Store> GetStore(string id);
        Task<bool> AddStore(Store store);
        Task DeleteStore(string id);
        Task<Store> UpdateStore(string id, Store store);

    }
}
