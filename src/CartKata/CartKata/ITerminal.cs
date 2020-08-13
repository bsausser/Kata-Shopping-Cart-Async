namespace CartKata
{
    /// <summary>
    /// Kata required interface
    /// </summary>
    public interface ITerminal
    {
        void Scan(string item);

        decimal Total();

    }
}
