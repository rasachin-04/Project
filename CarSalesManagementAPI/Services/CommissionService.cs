namespace CarSalesManagementAPI.Services
{
    public class CommissionService
    {
        public decimal CalculateCommission(string brand, string carClass, decimal modelPrice, int previousYearSales)
        {
            decimal fixedCommission = GetFixedCommission(brand, modelPrice);
            decimal classCommission = GetClassCommission(brand, carClass, modelPrice);
            decimal additionalCommission = 0;

            if (previousYearSales > 500000 && carClass == "A-Class")
            {
                additionalCommission = 0.02m * (fixedCommission + classCommission);
            }

            return fixedCommission + classCommission + additionalCommission;
        }

        private decimal GetFixedCommission(string brand, decimal modelPrice)
        {
            switch (brand)
            {
                case "Audi":
                    return modelPrice > 25000 ? 800 : 0;
                case "Jaguar":
                    return modelPrice > 35000 ? 750 : 0;
                case "Land Rover":
                    return modelPrice > 30000 ? 850 : 0;
                case "Renault":
                    return modelPrice > 20000 ? 400 : 0;
                default:
                    return 0;
            }
        }

        private decimal GetClassCommission(string brand, string carClass, decimal modelPrice)
        {
            decimal classCommissionRate = 0;

            switch (carClass)
            {
                case "A-Class":
                    classCommissionRate = brand == "Audi" ? 0.08m :
                                          brand == "Jaguar" ? 0.06m :
                                          brand == "Land Rover" ? 0.07m :
                                          brand == "Renault" ? 0.05m : 0;
                    break;
                case "B-Class":
                    classCommissionRate = brand == "Audi" ? 0.06m :
                                          brand == "Jaguar" ? 0.05m :
                                          brand == "Land Rover" ? 0.05m :
                                          brand == "Renault" ? 0.03m : 0;
                    break;
                case "C-Class":
                    classCommissionRate = brand == "Audi" ? 0.04m :
                                          brand == "Jaguar" ? 0.03m :
                                          brand == "Land Rover" ? 0.04m :
                                          brand == "Renault" ? 0.02m : 0;
                    break;
            }

            return modelPrice * classCommissionRate;
        }
    }

}
