namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;

    public interface IFeederService
    {
        ICollection<Item> GetFeeds(string feedUrl, Feed.SourceType source);
    }
}
