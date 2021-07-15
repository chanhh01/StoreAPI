using Microsoft.AspNetCore.Mvc;
using StoreModelandRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        public IStoreRepo _storeRepo;

        public StoreController(IStoreRepo storeRepo)
        {
            _storeRepo = storeRepo;
        }

        [HttpGet]
        public async Task<List<Store>> GetStore()
        {
            List<Store> store = await _storeRepo.GetStore();
            return store;
        }



        [HttpGet("{id}", Name = "GetStore")]
        public async Task<Store> GetStore(string id)
        {
            Store store = await _storeRepo.GetStore(id);
            return store;
        }

        [HttpPost]
        public async Task<IActionResult> AddStore(Store store)
        {
            await _storeRepo.AddStore(store);
            return CreatedAtRoute("GetStore", new { id = store.id }, store);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(string id)
        {
            await _storeRepo.DeleteStore(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(string id, Store store)
        {
            await _storeRepo.UpdateStore(store.id, store);
            return NoContent();
        }
    }
}
