namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeederService
    {
        T GetFeedsAsync<T>(string feedUri, Feed.SourceType source);

        bool IsValidRssUri(string feedUri);
    }
}
