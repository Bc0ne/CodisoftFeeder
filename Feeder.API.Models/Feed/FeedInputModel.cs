using System;
using System.Collections.Generic;
using System.Text;

namespace Feeder.API.Models.Feed
{
    public class FeedInputModel
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public long CollectionId { get; set; }
    }
}
