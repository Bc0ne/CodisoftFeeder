namespace Feeder.Web.API.Controllers
{
    using System.Collections.Generic;
    using Feeder.API.Models.Collection;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Feeder.Web.API.Helpers;
    using Feeder.Core.Collection;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [Route("api/collections")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionController(ICollectionRepository collectionRepository)
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
        public async Task<IActionResult> NewCollectionAsync([FromBody] CollectionInputModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ValidateObjectResult(ModelState);
            }

            var collection = Collection.New(model.CollectionName);

            var id = await _collectionRepository.AddCollectionAsync(collection);

            return Ok(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCollectionAsync(long id, [FromBody] CollectionUpdateModel model)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid collection Id");
            }

            if (!ModelState.IsValid)
            {
                return new ValidateObjectResult(ModelState);
            }

            collection.UpdateCollection(model.CollectionName);

            await _collectionRepository.UpdateCollectionAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCollectionAsync(long id)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid Collection Id");
            }

            await _collectionRepository.DeleteCollectionAsync(collection);

            return NoContent();
        }
    }
}
