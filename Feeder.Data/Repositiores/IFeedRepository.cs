namespace Feeder.Data.Repositiores
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeedRepository
    {
        Task<Feed> GetFeedAsync(long feedId);

        Task AddFeedAsync(Feed feed);

        Task<IEnumerable<Feed>> GetCollectionFeedsAsync(long collectionId);

        Task UpdateFeedAsync();

        Task DeleteFeedAsync(Feed feed);
    }
}
