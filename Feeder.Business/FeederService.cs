namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using Microsoft.SyndicationFeed;
    using Microsoft.SyndicationFeed.Rss;
    using System.Collections.Generic;
    using System.Data;
    using System.Net;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using System.Linq;

    public class FeederService : IFeederService
    {
        //private readonly string _feedUri;

        public FeederService()
        {
            //_feedUri = feedUri;
        }

        public List<Item> GetFeedsAsync(string feedUri)
        {

            var webClient = new WebClient();

            var response =  webClient.DownloadString(feedUri);

            XDocument document = XDocument.Parse(response);

            var items = (from descendant in document.Descendants("item")
                         select new Item
                         {
                             Description = descendant.Element("description").Value,
                             Title = descendant.Element("title").Value,
                             PublishDate = descendant.Element("pubDate").Value
                             
                         }).ToList();

            return items;

            //            var feeds = new List<Item>();
            //using (var xmlReader = XmlReader.Create(_feedUri,new XmlReaderSettings() { Async = true }))
            //{
            //    var feedReader = new RssFeedReader(xmlReader);
            //    while (await feedReader.Read())
            //    {
            //        if (feedReader.ElementType == Microsoft.SyndicationFeed.SyndicationElementType.Item)
            //        {
            //            ISyndicationItem item = await feedReader.ReadItem();

            //            feeds.Add(SyndicationExtensions.ConvertToItem(item));
            //        }
            //    }
            //}

        }
    }
}
