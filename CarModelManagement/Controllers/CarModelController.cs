using CarModelManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarModelManagement.Controllers
{
	using CarModelManagement.Models;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	[ApiController]
	[Route("api/[controller]")]
	public class CarModelController : ControllerBase
	{
		private readonly CarModelRepository _repository;

		public CarModelController(CarModelRepository repository)
		{
			_repository = repository;
		}

		
		[HttpGet]
		public async Task<IActionResult> GetCarModels()
		{
			var carModels = await _repository.GetAllCarModelsAsync();
			return Ok(carModels);
		}

		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCarModel(int id)
		{
			var carModels = await _repository.GetAllCarModelsAsync();
			var carModel = carModels.FirstOrDefault(c => c.Id == id);
			if (carModel == null)
			{
				return NotFound();
			}
			return Ok(carModel);
		}

		
		[HttpPost]
		public async Task<IActionResult> CreateCarModel([FromBody] CarModel carModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			int newId = await _repository.CreateCarModelAsync(carModel);
			return CreatedAtAction(nameof(GetCarModel), new { id = newId }, carModel);
		}

	
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCarModel(int id, [FromBody] CarModel carModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingModel = await _repository.GetAllCarModelsAsync();
			if (existingModel == null)
			{
				return NotFound();
			}

			bool success = await _repository.UpdateCarModelAsync(id, carModel);
			if (!success)
			{
				return StatusCode(500, "An error occurred while updating the car model.");
			}

			return NoContent(); 
		}

		
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCarModel(int id)
		{
			var carModels = await _repository.GetAllCarModelsAsync();
			var carModel = carModels.FirstOrDefault(c => c.Id == id);
			if (carModel == null)
			{
				return NotFound();
			}

			bool success = await _repository.DeleteCarModelAsync(id);
			if (!success)
			{
				return StatusCode(500, "An error occurred while deleting the car model.");
			}

			return NoContent(); 
		}
	}


}
