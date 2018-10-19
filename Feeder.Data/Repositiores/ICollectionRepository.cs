namespace Feeder.Data.Repositiores
{
    using Feeder.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICollectionRepository
    {
        Task<long> AddCollectionAsync(Collection collection);
        Task<Collection> GetCollectionAsync(long collectionId);
        Task<List<Collection>> GetCollectionsAsync();
    }
}
