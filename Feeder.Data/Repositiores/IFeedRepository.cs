namespace Feeder.Data.Repositiores
{
    using Feeder.Data.Entities;
    using System.Threading.Tasks;

    public interface IFeedRepository
    {
        Task AddCollectionAsync(Feed feed);
    }
}
