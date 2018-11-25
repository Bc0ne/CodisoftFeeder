namespace Feeder.Core.Atom
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class AtomFeedService
    {
        public static ICollection<Item> GetAtomFeeds(string feedUrl)
        {
            XDocument document = XDocument.Load(feedUrl);

            var entries = (from entry in document.Root.Elements().Where(i => i.Name.LocalName == "entry")
                           select new Item
                           {
                               Description = entry.Elements().First(i => i.Name.LocalName == "content").Value,
                               Title = entry.Elements().First(i => i.Name.LocalName == "title").Value,
                               PublishDate = entry.Elements().First(i => i.Name.LocalName == "published").Value,
                               Link = entry.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value
                           }).ToList();
            return entries;
        }
    }
}
