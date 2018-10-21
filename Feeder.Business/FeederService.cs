namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Net;
    using System.Xml.Linq;
    using System.Linq;
    using System;

    public class FeederService : IFeederService
    {
        public ICollection<Item> GetFeeds(string feedUrl, Feed.SourceType source)
        {
            switch (source)
            {
                case Feed.SourceType.RSS:
                    return GetRSSFeeds(feedUrl);

                case Feed.SourceType.Atom:
                    return GetAtomFeeds(feedUrl);

                default:
                    throw new NotImplementedException();
            }
        }

        private ICollection<Item> GetRSSFeeds(string feedUrl)
        {
            XDocument document = XDocument.Load(feedUrl);

            var items = (from descendant in document.Root.Elements().Where(i => i.Name.LocalName == "item")
                         select new Item
                         {
                             Description = descendant.Elements().First(i => i.Name.LocalName == "description").Value,
                             Title = descendant.Elements().First(i => i.Name.LocalName == "title").Value,
                             PublishDate = descendant.Elements().First(i => i.Name.LocalName == "pubDate").Value,
                             Link = descendant.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value
                         }).ToList();
            return items;
        }

        private ICollection<Item> GetAtomFeeds(string feedUrl)
        {
            XDocument document = XDocument.Load(feedUrl);

            var items = (from descendant in document.Root.Elements().Where(i => i.Name.LocalName == "entry")
                         select new Item
                         {
                             Description = descendant.Elements().First( i => i.Name.LocalName == "content").Value,
                             Title = descendant.Elements().First(i => i.Name.LocalName == "title").Value,
                             PublishDate = descendant.Elements().First(i => i.Name.LocalName == "published").Value,
                             Link = descendant.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value
                         }).ToList();
            return items;
        }
    }
}
