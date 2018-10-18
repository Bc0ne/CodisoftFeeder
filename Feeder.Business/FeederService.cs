namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using Microsoft.SyndicationFeed;
    using Microsoft.SyndicationFeed.Rss;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    public class FeederService : IFeederService
    {
        private readonly string _feedUri;

        public FeederService(string feedUri)
        {
            _feedUri = feedUri;
        }

        public async Task GetFeedsAsync()
        {
            var feeds = new List<Item>();
            using (var xmlReader = XmlReader.Create(_feedUri,new XmlReaderSettings() { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);
                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == Microsoft.SyndicationFeed.SyndicationElementType.Item)
                    {
                        ISyndicationItem item = await feedReader.ReadItem();
                      
                        feeds.Add(SyndicationExtensions.ConvertToItem(item));
                    }
                }
            }
            
        }
    }
}
