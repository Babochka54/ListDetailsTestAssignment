namespace ListDetailsTest.Model
{
    public class Order
    {
        public DateOnly date { get; set; }
        public string? storeCity { get; set; }
        public int companyId { get; set; }
        public Company? company { get; set; }
    }
}
