namespace Feeder.Core.FeederServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Feed;

    public class RSSFeedService
    {
        public static ICollection<Item> GetRSSFeeds(string feedUrl)
        {
            XDocument document = XDocument.Load(feedUrl);

            var items = (from descendant in document.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                         select new Item
                         {
                             Description = descendant.Elements().First(i => i.Name.LocalName == "description").Value,
                             Title = descendant.Elements().First(i => i.Name.LocalName == "title").Value,
                             PublishDate = descendant.Elements().First(i => i.Name.LocalName == "pubDate").Value,
                             Link = descendant.Elements().First(i => i.Name.LocalName == "link").Value
                         }).ToList();
            return items;
        }
    }
}
