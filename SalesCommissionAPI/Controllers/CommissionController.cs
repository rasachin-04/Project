using Microsoft.AspNetCore.Mvc;
using SalesCommissionAPI.Models;
using SalesCommissionAPI.Repositories;

namespace SalesCommissionAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CommissionController : ControllerBase
	{
		private readonly SalesmanRepository _salesmanRepository;

		public CommissionController(SalesmanRepository salesmanRepository)
		{
			_salesmanRepository = salesmanRepository;
		}

		[HttpGet("salesmen")]
		public ActionResult<List<Salesman>> GetAllSalesmen()
		{
			return Ok(_salesmanRepository.GetAllSalesmen());
		}

		[HttpPost("addSalesman")]
		public IActionResult AddSalesman([FromBody] Salesman salesman)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_salesmanRepository.AddSalesman(salesman);
			return Ok("Salesman added successfully");
		}

		[HttpGet("salesman/{id}")]
		public ActionResult<Salesman> GetSalesmanById(int id)
		{
			var salesman = _salesmanRepository.GetSalesmanById(id);
			if (salesman == null)
			{
				return NotFound();
			}
			return Ok(salesman);
		}
	}
}
