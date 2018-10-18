namespace Feeder.Core
{
    using System.Threading.Tasks;

    public interface IFeederService
    {
        Task GetFeedsAsync();
    }
}
