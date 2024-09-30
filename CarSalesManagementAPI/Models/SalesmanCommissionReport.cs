namespace CarSalesManagementAPI.Models
{
    public class SalesmanCommissionReport
    {
        public string SalesmanName { get; set; }
        public string CarBrand { get; set; }
        public string CarClass { get; set; }
        public int NumberOfCarsSold { get; set; }
        public decimal ModelPrice { get; set; }
        public decimal Commission { get; set; }
        public decimal AdditionalCommission { get; set; }
        public decimal TotalCommission { get; set; }
    }
}
