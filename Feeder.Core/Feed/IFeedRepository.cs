namespace Feeder.Core.Feed
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeedRepository
    {
        Task<Feed> GetFeedAsync(long feedId);

        Task AddFeedAsync(Feed feed);

        Task UpdateFeedAsync();

        Task DeleteFeedAsync(Feed feed);
    }
}
