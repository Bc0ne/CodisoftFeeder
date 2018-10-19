﻿namespace Feeder.Web.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Feeder.API.Models.Collection;
    using Feeder.API.Models.Feed;
    using Feeder.Core;
    using Feeder.Data.Entities;
    using Feeder.Data.Repositiores;
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
        [ResponseCache(VaryByQueryKeys = new string[] {"id"}, Duration = 60)]
        public async Task<IActionResult> GetCollectionNews(long id)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if(collection == null)
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

                var items = _feederService.GetFeedsAsync<List<Item>>(feed.Link, feed.Type);

                foreach (var item in items)
                {
                    feedOutputModel.Items.Add(new ItemOutputModel
                    {
                        Title = item.Title,
                        Description = item.Description,
                        PublishDate = item.PublishDate
                    });
                }
                response.Feeds.Add(feedOutputModel);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("new")]
       public async Task<IActionResult> AddFeedAsync(long collectionId, [FromBody] FeedInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var collection = await _collectionRepository.GetCollectionAsync(collectionId);

            if (collection == null)
            {
                return NotFound("Invalid collection id");
            }

            if (!_feederService.IsValidRssUri(model.Link))
            {
                return NotFound("Invalid Rss Uri");
            }

            var feed = Feed.New(model.Title, model.Link, model.SourceType, collection);

            await _feedRepository.AddFeedAsync(feed);

            return Ok();
        }


    }
}
