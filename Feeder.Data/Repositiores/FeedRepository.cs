namespace Feeder.Data.Repositiores
{
    using System.Threading.Tasks;
    using Feeder.Core.Feed;
    using Feeder.Data.Context;
    using Microsoft.EntityFrameworkCore;

    public class FeedRepository : IFeedRepository
    {
        private readonly FeederContext _context;

        public FeedRepository(FeederContext context)
        {
            _context = context;
        }

        public async Task<Feed> GetFeedAsync(long feedId)
        {
            return await _context.Feeds.SingleOrDefaultAsync(x => x.Id == feedId);
        }

        public async Task AddFeedAsync(Feed feed)
        {
            await _context.Feeds.AddAsync(feed);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedAsync(Feed feed)
        {
            _context.Feeds.Remove(feed);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
