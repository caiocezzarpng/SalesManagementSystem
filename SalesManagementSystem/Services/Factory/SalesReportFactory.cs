using SalesManagementSystem.Models;
using SalesManagementSystem.Services.Factory.Interface;
using SalesManagementSystem.Services.Reports;

namespace SalesManagementSystem.Services.Factory
{
    public class SalesReportFactory : ReportFactory<SalesRecord>
    {
        private readonly IConfiguration _configuration;

        public SalesReportFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override IReport<SalesRecord> CreateReport()
        {
            return new SaleReport(_configuration);
        }
    }
}
