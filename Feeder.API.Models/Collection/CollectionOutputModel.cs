﻿namespace Feeder.API.Models.Collection
{
    using Feeder.API.Models.Feed;
    using System.Collections.Generic;

    public class CollectionOutputModel
    {
        public CollectionOutputModel()
        {
            Feeds = new List<FeedOutputModel>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public List<FeedOutputModel> Feeds { get; set; }
    }
}
