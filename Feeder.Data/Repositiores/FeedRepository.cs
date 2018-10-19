namespace Feeder.Data.Repositiores
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Feeder.Data.Context;
    using Feeder.Data.Entities;

    public class FeedRepository : IFeedRepository
    {
        private readonly FeederContext _context;

        public FeedRepository(FeederContext context)
        {
            _context = context;
        }

        public async Task AddFeedAsync(Feed feed)
        {
            await _context.Feeds.AddAsync(feed);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Feed>> GetCollectionFeedsAsync(long collectionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
