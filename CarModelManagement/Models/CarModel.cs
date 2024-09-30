using System.ComponentModel.DataAnnotations;

namespace CarModelManagement.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class CarModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Brand is required")]
		public string? Brand { get; set; }

		[Required(ErrorMessage = "Class is required")]
		public string? Class { get; set; }

		[Required(ErrorMessage = "Model Name is required")]
		[StringLength(100, ErrorMessage = "Model Name must be less than 100 characters")]
		public string? ModelName { get; set; }

		[Required(ErrorMessage = "Model Code is required")]
		[RegularExpression(@"^[a-zA-Z0-9]{10}$", ErrorMessage = "Model Code must be exactly 10 alphanumeric characters")]
		public string? ModelCode { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Features are required")]
		public string? Features { get; set; }

		[Required(ErrorMessage = "Price is required")]
		[Range(0, 10000000, ErrorMessage = "Price must be between 0 and 10,000,000")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Date of Manufacturing is required")]
		[DataType(DataType.Date, ErrorMessage = "Invalid Date")]
		public DateTime DateOfManufacturing { get; set; }

		[Required(ErrorMessage = "Active status is required")]
		public bool Active { get; set; }

		[Required(ErrorMessage = "Sort order is required")]
		[Range(1, 100, ErrorMessage = "Sort order must be between 1 and 100")]
		public int SortOrder { get; set; }

		[Required(ErrorMessage = "Model Images are required")]
		public string[]? ModelImages { get; set; }
	}


}
