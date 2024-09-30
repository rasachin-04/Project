using System.ComponentModel.DataAnnotations;

namespace SalesCommissionAPI.Models
{
	public class CarSale
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Salesman Id is required")]
		public int SalesmanId { get; set; }

		[Required(ErrorMessage = "Brand is required")]
		public string Brand { get; set; }

		[Required(ErrorMessage = "Car class is required")]
		public string CarClass { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Number of cars sold must be positive")]
		public int NumberOfCarsSold { get; set; }
	}
}

