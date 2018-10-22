namespace Feeder.Web.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Feeder.API.Models.Collection;
    using Feeder.API.Models.Feed;
    using Feeder.Core;
    using Feeder.Data.Entities;
    using Feeder.Data.Repositiores;
    using Feeder.Web.API.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    [Route("api/collections/{id}/feeds")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IFeedRepository _feedRepository;
        private readonly IFeederService _feederService;

        public FeedController(ICollectionRepository collectionRepository, IFeedRepository feedRepository, IFeederService feederService)
        {
            _collectionRepository = collectionRepository;
            _feedRepository = feedRepository;
            _feederService = feederService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectionNews(long id)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid Collection Id");
            }

            var response = new CollectionNewsOutputModel();

            response.Id = collection.Id;
            response.Name = collection.Name;

            foreach (var feed in collection.Feeds)
            {
                var feedOutputModel = new FeedOutputModel();
                feedOutputModel.Id = feed.Id;
                feedOutputModel.Title = feed.Title;
                feedOutputModel.Link = feed.Link;
                feedOutputModel.Type = feed.Type.ToString();

                var items =  _feederService.GetFeeds(feed.Link, feed.Type);

                foreach (var item in items)
                {
                    feedOutputModel.Items.Add(new ItemOutputModel
                    {
                        Title = item.Title,
                        Description = item.Description,
                        PublishDate = item.PublishDate,
                        Link = item.Link
                    });
                }
                response.Feeds.Add(feedOutputModel);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{feedId}")]
        public async Task<IActionResult> GetFeedNews(long id, long feedId)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid Collection Id");
            }

            var feed = await _feedRepository.GetFeedAsync(feedId);

            if (feed == null)
            {
                return NotFound("Invalid Feed Id");
            }

            var response = new FeedOutputModel();

            response.Id = feed.Id;
            response.Title = feed.Title;
            response.Link = feed.Link;
            response.Type = feed.Type.ToString();

            var items =  _feederService.GetFeeds(feed.Link, feed.Type);

            foreach (var item in items)
            {
                response.Items.Add(new ItemOutputModel
                {
                    Title = item.Title,
                    Description = item.Description,
                    PublishDate = item.PublishDate,
                    Link = item.Link
                });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedAsync(long id, [FromBody] FeedInputModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new ValidateObjectResult(ModelState);
            }

            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid Collection Id");
            }

            var feed = Feed.New(model.Title, model.Link, model.SourceType, collection);

            await _feedRepository.AddFeedAsync(feed);

            return Ok();
        }

        [HttpPut]
        [Route("{feedId}")]
        public async Task<IActionResult> UpdateFeedAsync(long id, long feedId, [FromBody] FeedUpdateModel model)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if (collection == null)
            {
                return NotFound("Invalid Collection Id");
            }

            var feed = await _feedRepository.GetFeedAsync(feedId);

            if (feed == null)
            {
                return NotFound("Invalid Feed Id");
            }

            if (!ModelState.IsValid)
            {
                return new ValidateObjectResult(ModelState);
            }

            feed.UpdateTitle(model.Title);

            await _feedRepository.UpdateFeedAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{feedId}")]
        public async Task<IActionResult> DeleteFeedAsync(long id, long feedId)
        {
            var feed = await _feedRepository.GetFeedAsync(feedId);

            if (feed == null)
            {
                return NotFound("Invalid Feed Id");
            }

            await _feedRepository.DeleteFeedAsync(feed);

            return NoContent();
        }
    }
}
