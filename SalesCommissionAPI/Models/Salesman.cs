using System.ComponentModel.DataAnnotations;

namespace SalesCommissionAPI.Models
{
	public class Salesman
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(50, ErrorMessage = "Name can't exceed 50 characters")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Role is required")]
		public string? Role { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Sales amount must be positive")]
		public decimal PreviousYearSales { get; set; }
	}
}
