namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System;
    using Feeder.Core.RSS;
    using Feeder.Core.Atom;

    public class FeederService : IFeederService
    {
        public ICollection<Item> GetFeeds(string feedUrl, Feed.SourceType source)
        {
            switch (source)
            {
                case Feed.SourceType.RSS:
                    return RSSFeedService.GetRSSFeeds(feedUrl);

                case Feed.SourceType.Atom:
                    return AtomFeedService.GetAtomFeeds(feedUrl);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
