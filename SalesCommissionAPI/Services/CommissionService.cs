using SalesCommissionAPI.Models;

namespace SalesCommissionAPI.Services
{
	public class CommissionService
	{
		public decimal CalculateCommission(Salesman salesman, List<CarSale> carSales)
		{
			decimal totalCommission = 0;

			foreach (var sale in carSales)
			{
				decimal fixedCommission = 0;
				decimal classCommission = 0;

				switch (sale.Brand)
				{
					case "Audi":
						fixedCommission = 800;
						classCommission = sale.CarClass switch
						{
							"A" => 0.08m,
							"B" => 0.06m,
							"C" => 0.04m,
							_ => 0
						};
						break;
					case "Jaguar":
						fixedCommission = 750;
						classCommission = sale.CarClass switch
						{
							"A" => 0.06m,
							"B" => 0.05m,
							"C" => 0.03m,
							_ => 0
						};
						break;
					case "Land Rover":
						fixedCommission = 850;
						classCommission = sale.CarClass switch
						{
							"A" => 0.07m,
							"B" => 0.05m,
							"C" => 0.04m,
							_ => 0
						};
						break;
					case "Renault":
						fixedCommission = 400;
						classCommission = sale.CarClass switch
						{
							"A" => 0.05m,
							"B" => 0.03m,
							"C" => 0.02m,
							_ => 0
						};
						break;
				}

				totalCommission += fixedCommission + (sale.NumberOfCarsSold * classCommission);
			}

			if (salesman.PreviousYearSales > 500000 && carSales.Any(cs => cs.CarClass == "A"))
			{
				totalCommission += totalCommission * 0.02m;
			}

			return totalCommission;
		}
	}
}
