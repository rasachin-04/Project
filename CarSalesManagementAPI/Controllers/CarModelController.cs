using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CarManagementAPI.Models;
using CarManagementAPI.Services;
using CarSalesManagementAPI.Models;

namespace CarManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        private readonly CarModelService _carModelService;

        public CarModelController(CarModelService carModelService)
        {
            _carModelService = carModelService;

           
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<CarModel>> GetAllCarModels()
        {
            var carModels = _carModelService.GetAllCarModels();
            return Ok(carModels);
        }

        
        [HttpGet("{id}")]
        public ActionResult<CarModel> GetCarModelById(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return Ok(carModel);
        }

        // POST: api/CarModel
        [HttpPost]
        public ActionResult AddCarModel([FromBody] CarModel newCarModel)
        {
            _carModelService.AddCarModel(newCarModel);
            return CreatedAtAction(nameof(GetCarModelById), new { id = newCarModel.ModelCode }, newCarModel);
        }

        // PUT: api/CarModel/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCarModel(int id, [FromBody] CarModel updatedCarModel)
        {
            var result = _carModelService.UpdateCarModel(id, updatedCarModel);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/CarModel/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCarModel(int id)
        {
            var result = _carModelService.DeleteCarModel(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/CarModel/upload-images
        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadCarModelImages(List<IFormFile> modelImages)
        {
            if (modelImages == null || modelImages.Count == 0)
            {
                return BadRequest("No images provided.");
            }

            var uploadedFilePaths = new List<string>();

            foreach (var image in modelImages)
            {
                if (image.Length > 0)
                {
                    // Check the file size (5MB max)
                    if (image.Length > 5 * 1024 * 1024)
                    {
                        return BadRequest("File size exceeds the maximum allowed size of 5MB.");
                    }

                    // Check the file type (Only images allowed)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(image.FileName).ToLower();
                    if (Array.IndexOf(allowedExtensions, extension) == -1)
                    {
                        return BadRequest("Invalid file type. Only images are allowed.");
                    }

                    // Generate a unique file name
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var filePath = Path.Combine(_imageFolderPath, uniqueFileName);

                    // Save the image to the file system
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Store the file path for return
                    uploadedFilePaths.Add(Path.Combine("uploads", uniqueFileName));
                }
            }

            return Ok(new { UploadedPaths = uploadedFilePaths });
        }
    }
}
