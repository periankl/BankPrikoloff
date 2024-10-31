namespace BankPrikoloff.Contracts
{
    public class CreateCurrencyRequest
    {
        public string Name { get; set; } = null!;
        public decimal Course { get; set; }
    }
}