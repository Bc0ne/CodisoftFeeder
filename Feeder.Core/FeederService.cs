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
        public T GetFeedsAsync<T>(string feedUri, Feed.SourceType source)
        {
            switch(source)
            {
                case Feed.SourceType.RSS:
                    return (T) Convert.ChangeType(GetRssFeeds(feedUri), typeof(T));

                default:
                    throw new NotImplementedException();
            }
        }

        private List<Item> GetRssFeeds(string feedUri)
        {
            var webClient = new WebClient();

            var response = webClient.DownloadString(feedUri);

            XDocument document = XDocument.Parse(response);

            var items = (from descendant in document.Descendants("item")
                         select new Item
                         {
                             Description = descendant.Element("description").Value,
                             Title = descendant.Element("title").Value,
                             PublishDate = descendant.Element("pubDate").Value

                         }).ToList();

            return items;
        }

        public bool IsValidRssUri(string feedUri)
        {
            try
            {
                var webClient = new WebClient();

                var response = webClient.DownloadString(feedUri);

                XDocument document = XDocument.Parse(response);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
