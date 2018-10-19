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
        public List<Item> GetFeedsAsync(string feedUri)
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
