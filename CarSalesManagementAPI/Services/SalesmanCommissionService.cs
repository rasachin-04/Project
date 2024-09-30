using System;
using System.Collections.Generic;
using CarManagementAPI.Models;
using CarSalesManagementAPI.Models;

namespace CarManagementAPI.Services
{
    public class SalesmanCommissionService
    {
        public List<SalesmanCommissionReport> GetCommissionReport(List<SalesData> salesData, List<CarModel> carModels, List<Salesman> salesmen)
        {
            List<SalesmanCommissionReport> report = new List<SalesmanCommissionReport>();

            foreach (var salesman in salesmen)
            {
                foreach (var sale in salesData)
                {
                    if (sale.SalesmanName == salesman.Name)
                    {
                        var carModel = carModels.Find(c => c.Brand == sale.Brand && c.Class == sale.CarClass);

                        if (carModel != null)
                        {
                            decimal baseCommission = GetBaseCommission(sale.Brand, sale.CarClass, carModel.Price);
                            decimal additionalCommission = GetAdditionalCommission(sale.CarClass, carModel.Price);
                            decimal totalCommission = baseCommission + additionalCommission;

                            // If the salesman sold more than $500,000 last year and the car is class A, give 2% additional commission
                            if (salesman.LastYearSales > 500000 && sale.CarClass == "A-Class")
                            {
                                totalCommission += carModel.Price * 0.02m;
                            }

                            report.Add(new SalesmanCommissionReport
                            {
                                SalesmanName = sale.SalesmanName,
                                CarBrand = sale.Brand,
                                CarClass = sale.CarClass,
                                NumberOfCarsSold = sale.NumberOfCarsSold,
                                ModelPrice = carModel.Price,
                                Commission = baseCommission,
                                AdditionalCommission = additionalCommission,
                                TotalCommission = totalCommission
                            });
                        }
                    }
                }
            }

            return report;
        }

        private decimal GetBaseCommission(string brand, string carClass, decimal price)
        {
            decimal commission = 0;
            switch (brand)
            {
                case "Audi":
                    if (price > 25000)
                        commission = carClass == "A-Class" ? 800m : carClass == "B-Class" ? 6m : 4m;
                    break;
                case "Jaguar":
                    if (price > 35000)
                        commission = carClass == "A-Class" ? 750m : carClass == "B-Class" ? 5m : 3m;
                    break;
                case "Land Rover":
                    if (price > 30000)
                        commission = carClass == "A-Class" ? 850m : carClass == "B-Class" ? 5m : 4m;
                    break;
                case "Renault":
                    if (price > 20000)
                        commission = carClass == "A-Class" ? 400m : carClass == "B-Class" ? 3m : 2m;
                    break;
            }
            return commission;
        }

        private decimal GetAdditionalCommission(string carClass, decimal price)
        {
            decimal percentage = carClass == "A-Class" ? 0.08m : carClass == "B-Class" ? 0.05m : 0.03m;
            return price * percentage;
        }
    }
}
