namespace Feeder.Data.Repositiores
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeedRepository
    {
        Task AddFeedAsync(Feed feed);
        Task<IEnumerable<Feed>> GetCollectionFeedsAsync(long collectionId);
    }
}
