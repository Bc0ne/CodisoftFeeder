namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using Microsoft.SyndicationFeed;
    using System.Linq;

    public static class SyndicationExtensions
    {
        public static Item ConvertToItem(ISyndicationItem item)
        {
            return new Item
            {
                Title = item.Title,
                Description = item.Description,
                PublishDate = item.Published,
                Link = item.Links.First().Uri.AbsoluteUri,
            };
        }
    }
}
