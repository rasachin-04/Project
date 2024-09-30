using System.Collections.Generic;
using System.Linq;
using CarManagementAPI.Models;
using CarSalesManagementAPI.Models;

namespace CarManagementAPI.Services
{
    public class CarModelService
    {
        private List<CarModel> _carModels = new List<CarModel>
        {
            new CarModel { Brand = "Audi", Class = "A-Class", ModelName = "A3", ModelCode = "AUD123456", Description = "Compact luxury sedan", Features = "Premium interior, safety features", Price = 25000, DateOfManufacturing = DateTime.Now, Active = true, SortOrder = 1 },
            // Add more sample data if needed
        };

        public List<CarModel> GetAllCarModels()
        {
            return _carModels;
        }

        public CarModel GetCarModelById(int id)
        {
            return _carModels.FirstOrDefault(c => c.ModelCode == id.ToString());
        }

        public void AddCarModel(CarModel newCarModel)
        {
            _carModels.Add(newCarModel);
        }

        public bool UpdateCarModel(int id, CarModel updatedCarModel)
        {
            var carModel = _carModels.FirstOrDefault(c => c.ModelCode == id.ToString());
            if (carModel == null)
            {
                return false;
            }

            carModel.Brand = updatedCarModel.Brand;
            carModel.Class = updatedCarModel.Class;
            carModel.ModelName = updatedCarModel.ModelName;
            carModel.Description = updatedCarModel.Description;
            carModel.Features = updatedCarModel.Features;
            carModel.Price = updatedCarModel.Price;
            carModel.DateOfManufacturing = updatedCarModel.DateOfManufacturing;

            carModel.Active = updatedCarModel.Active;
            carModel.SortOrder = updatedCarModel.SortOrder;

            return true;
        }

        public bool DeleteCarModel(int id)
        {
            var carModel = _carModels.FirstOrDefault(c => c.ModelCode == id.ToString());
            if (carModel == null)
            {
                return false;
            }

            _carModels.Remove(carModel);
            return true;
        }
    }
}
