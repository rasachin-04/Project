using Microsoft.AspNetCore.Mvc;
using CarManagementAPI.Models;
using CarManagementAPI.Services;
using System.Collections.Generic;
using CarSalesManagementAPI.Models;

namespace CarManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesmanCommissionController : ControllerBase
    {
        private readonly SalesmanCommissionService _commissionService;

        public SalesmanCommissionController(SalesmanCommissionService commissionService)
        {
            _commissionService = commissionService;
        }

        [HttpPost("generateReport")]
        public ActionResult<List<SalesmanCommissionReport>> GenerateReport([FromBody] CommissionReportRequest request)
        {
            var report = _commissionService.GetCommissionReport(request.SalesData, request.CarModels, request.Salesmen);
            return Ok(report);
        }
    }
}
