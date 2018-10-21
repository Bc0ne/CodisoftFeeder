namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeederService
    {
        ICollection<Item> GetFeeds(string feedUrl, Feed.SourceType source);
    }
}
