namespace Dollar.Interfaces.Services
{
    public interface IDollarService
    {
        Task<decimal> GetDollarExchangeRate();
    }
}
