namespace Feeder.Data.Entities
{
    using System;

    public class Item
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public Feed Feed { get; set; }
    }
}
