namespace Feeder.API.Models.Feed
{
    using System.Collections.Generic;

    public class FeedOutputModel
    {
        public FeedOutputModel()
        {
            Items = new List<ItemOutputModel>();
        }

        public long Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Type { get; set; }

        public List<ItemOutputModel> Items { get; set; }


    }
}
