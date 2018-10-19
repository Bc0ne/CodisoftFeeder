namespace Feeder.Core
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeederService
    {
        List<Item> GetFeedsAsync(string feedUri);

        bool IsValidRssUri(string feedUri);
    }
}
