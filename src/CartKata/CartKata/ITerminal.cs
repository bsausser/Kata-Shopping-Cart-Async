using System.Threading.Tasks;

namespace CartKata
{
    /// <summary>
    /// Kata required interface
    /// </summary>
    public interface ITerminal
    {
        Task ScanAsync(string item);

        Task<decimal> TotalAsync();

    }
}
