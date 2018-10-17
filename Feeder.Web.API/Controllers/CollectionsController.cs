using System.Collections.Generic;
namespace Feeder.Web.API.Controllers
{
    using Feeder.API.Models.Collection;
    using Feeder.Data.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/collections")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionsController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
        
        [HttpGet()]
        public IActionResult GetCollections()
        {
            List<string> collections = new List<string>();
            collections.Add("Tech");
            collections.Add("Sport");

            return Ok(collections);
        }

        [HttpPost]
        [Route("new")]
        public async Task<long> NewCollectionAsync([FromBody] CollectionInputModel model)
        {
            var collection = Collection.New(model.CollectionName);

            var id = await _collectionRepository.AddCollectionAsync(collection);

            return id;
        }
       
    }
}
