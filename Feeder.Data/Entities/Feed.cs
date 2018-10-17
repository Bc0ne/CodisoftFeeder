namespace Feeder.Data.Entities
{
    using System.Collections.Generic;

    public class Feed
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string CopyRight { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public FeedImage Image { get; set; }
        //public ICollection<Item> Feeds { get; set; }
    }
}
