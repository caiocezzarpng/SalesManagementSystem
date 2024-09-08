using SalesManagementSystem.Models;
using SalesManagementSystem.Services.Factory.Interface;
using SalesManagementSystem.Services.Reports;

namespace SalesManagementSystem.Services.Factory
{
    public class SellersReportFactory : ReportFactory<Seller>
    {
        private readonly IConfiguration _configuration;

        public SellersReportFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override IReport<Seller> CreateReport()
        {
            return new SellerReport(_configuration);
        }
    }
}
