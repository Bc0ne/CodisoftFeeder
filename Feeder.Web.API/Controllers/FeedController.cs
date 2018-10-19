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
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var collection = await _collectionRepository.GetCollectionAsync(id);

            if(collection == null)
            {
                return NotFound("Invalid Collection Id");
            }

            var response = new CollectionOutputModel();

            response.Id = collection.Id;
            response.Name = collection.Name;
            foreach (var feed in collection.Feeds)
            {
                var feedOutputModel = new FeedOutputModel();
                feedOutputModel.Id = feed.Id;
                feedOutputModel.Title = feed.Title;
                feedOutputModel.Link = feed.Link;

                var items = _feederService.GetFeedsAsync(feed.Link);

                foreach (var item in items)
                {
                    var itemOutputModel = new ItemOutputModel();
                    itemOutputModel.Title = item.Title;
                    itemOutputModel.Description = item.Description;
                    itemOutputModel.PublishDate = item.PublishDate;

                    feedOutputModel.Items.Add(itemOutputModel);
                }

                response.Feeds.Add(feedOutputModel);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("new")]
       public async Task<IActionResult> AddFeedAsync(FeedInputModel model)
        {
            var collection = await _collectionRepository.GetCollectionAsync(model.CollectionId);

            if (collection == null)
            {
                return NotFound("Invalid collection id");
            }

            var feed = Feed.New(model.Title, model.Link, collection);

            await _feedRepository.AddFeedAsync(feed);

            return Ok();
        }
    }
}
