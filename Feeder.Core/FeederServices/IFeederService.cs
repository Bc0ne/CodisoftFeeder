namespace Feeder.Core.FeederServices
{
    using System.Collections.Generic;
    using Feed;

    public interface IFeederService
    {
        ICollection<Item> GetFeeds(string feedUrl, Feed.SourceType source);
    }
}
