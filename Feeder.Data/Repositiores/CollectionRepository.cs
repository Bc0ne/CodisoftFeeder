namespace Feeder.Data.Repositiores
{
    using Feeder.Core.Collection;
    using Feeder.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CollectionRepository : ICollectionRepository
    {
        private readonly FeederContext _context;

        public CollectionRepository(FeederContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> AddCollectionAsync(Collection collection)
        {
            await _context.Collections.AddAsync(collection);
            await _context.SaveChangesAsync();

            return collection.Id;
        }

        public async Task DeleteCollectionAsync(Collection collecton)
        {
            _context.Collections.Remove(collecton);
            await _context.SaveChangesAsync();
        }

        public async Task<Collection> GetCollectionAsync(long id)
        {
            return await _context.Collections.Include(x => x.Feeds).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Collection>> GetCollectionsAsync()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task UpdateCollectionAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
