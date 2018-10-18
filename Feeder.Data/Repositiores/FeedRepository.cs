using System.Threading.Tasks;
using Feeder.Data.Context;
using Feeder.Data.Entities;

namespace Feeder.Data.Repositiores
{
    public class FeedRepository : IFeedRepository
    {
        private readonly FeederContext _context;

        public FeedRepository(FeederContext context)
        {
            _context = context;
        }

        public async Task AddCollectionAsync(Feed feed)
        {
            await _context.Feeds.AddAsync(feed);
            await _context.SaveChangesAsync();
        }
    }
}
