using System;
using System.ComponentModel.DataAnnotations;

namespace CarSalesManagementAPI.Models
{
	public class CarModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Brand is required.")]
		[RegularExpression(@"^(Audi|Jaguar|Land Rover|Renault)$", ErrorMessage = "Brand must be one of the following: Audi, Jaguar, Land Rover, Renault.")]
		public string Brand { get; set; } // Audi, Jaguar, etc.

		[Required(ErrorMessage = "Class is required.")]
		[RegularExpression(@"^(A-Class|B-Class|C-Class)$", ErrorMessage = "Class must be one of the following: A-Class, B-Class, C-Class.")]
		public string Class { get; set; } // A-Class, B-Class, C-Class

		[Required(ErrorMessage = "Model Name is required.")]
		[StringLength(100, ErrorMessage = "Model Name cannot exceed 100 characters.")]
		public string ModelName { get; set; }

		[Required(ErrorMessage = "Model Code is required.")]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Model Code must be exactly 10 alphanumeric characters.")]
		[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Model Code must be alphanumeric.")]
		public string ModelCode { get; set; } // 10 Alphanumeric characters

		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Features are required.")]
		public string Features { get; set; }

		[Required(ErrorMessage = "Price is required.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Date of Manufacturing is required.")]
		[DataType(DataType.Date, ErrorMessage = "Date of Manufacturing must be a valid date.")]
		public DateTime DateOfManufacturing { get; set; }

		[Required(ErrorMessage = "Date of Manufacture is required.")]
		[DataType(DataType.Date, ErrorMessage = "Date of Manufacture must be a valid date.")]
		public DateTime DateOfManufacture { get; set; }

		public bool Active { get; set; } // Boolean does not require validation

		[Required(ErrorMessage = "Sort Order is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Sort Order must be a positive number.")]
		public int SortOrder { get; set; }
	}
}

