using SalesManagementSystem.Models;
using SalesManagementSystem.Services.Factory.Interface;

namespace SalesManagementSystem.Services.Reports
{
    public class DepartmentReport : IReport<Department>
    {
        private readonly IConfiguration _configuration;

        public DepartmentReport(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Generate(List<Department> departments)
        {
            var fileName = $"\\report-{DateTime.Now:yyyyMMddHHmmss}.txt";

            var directoryPath = Path.GetDirectoryName(_configuration.GetValue<string>("FilePaths:DepartmentsReportPath"));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var writer = new StreamWriter(directoryPath + fileName);
            writer.WriteLine("Departments Report");
            writer.WriteLine("--------------------");
            writer.WriteLine();

            foreach (var record in departments)
            {
                writer.WriteLine($"ID: {record.Id}");
                writer.WriteLine($"Name: {record.Name}");
                writer.WriteLine("--------------------");
            }
        }
    }
}