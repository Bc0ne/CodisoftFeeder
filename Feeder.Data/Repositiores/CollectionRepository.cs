namespace Feeder.Data.Repositiores
{
    using Feeder.Data.Context;
    using Feeder.Data.Entities;
    using System;
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
    }
}
