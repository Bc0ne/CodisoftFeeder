namespace Feeder.Web.API.Controllers
{
    using System.Collections.Generic;
    using Feeder.API.Models.Collection;
    using Feeder.Data.Entities;
    using Feeder.Data.Repositiores;
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

        [HttpGet]
        public async Task<IActionResult> GetCollectionsAsync()
        {
            var collections = await _collectionRepository.GetCollectionsAsync();

            var response = new List<CollectionOutputModel>();
            foreach (var c in collections)
            {
                response.Add(new CollectionOutputModel
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> NewCollectionAsync([FromBody] CollectionInputModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var collection = Collection.New(model.CollectionName);

            var id = await _collectionRepository.AddCollectionAsync(collection);

            return Ok(id);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCollectionAsync(long id, [FromBody] CollectionUpdateModel model)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid collection Id");
            }

            if (string.IsNullOrEmpty(model.CollectionName))
            {
                return BadRequest("Name can't be null or empty");
            }

            collection.UpdateCollection(model.CollectionName);

            await _collectionRepository.UpdateCollectionAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCollectionAsync(long id)
        {

            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            await _collectionRepository.DeleteCollectionAsync(collection);

            return NoContent();
        }
       
    }
}
