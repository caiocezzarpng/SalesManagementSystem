using SalesManagementSystem.Models;
using SalesManagementSystem.Services.Factory.Interface;
using SalesManagementSystem.Services.Reports;

namespace SalesManagementSystem.Services.Factory
{
    public class DepartmentsReportFactory : ReportFactory<Department>
    {
        private readonly IConfiguration _configuration;

        public DepartmentsReportFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override IReport<Department> CreateReport()
        {
            return new DepartmentReport(_configuration);
        }
    }
}
