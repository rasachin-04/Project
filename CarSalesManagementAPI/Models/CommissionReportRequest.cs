using CarSalesManagementAPI.Models;
using System.Collections.Generic;

namespace CarManagementAPI.Models
{
    public class CommissionReportRequest
    {
        public List<SalesData> SalesData { get; set; }
        public List<CarModel> CarModels { get; set; }
        public List<Salesman> Salesmen { get; set; }
    }
}
