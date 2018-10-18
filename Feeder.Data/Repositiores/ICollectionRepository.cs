namespace Feeder.Data.Repositiores
{
    using Feeder.Data.Entities;
    using System.Threading.Tasks;

    public interface ICollectionRepository
    {
        Task<long> AddCollectionAsync(Collection collection);
    }
}
