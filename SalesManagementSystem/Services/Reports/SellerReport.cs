using SalesManagementSystem.Models;
using SalesManagementSystem.Services.Factory.Interface;

namespace SalesManagementSystem.Services.Reports
{
    public class SellerReport : IReport<Seller>
    {
        private readonly IConfiguration _configuration;

        public SellerReport(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Generate(List<Seller> sellers)
        {
            var fileName = $"\\report-{DateTime.Now:yyyyMMddHHmmss}.txt";

            // Create the directory if it doesn't exist
            var directoryPath = Path.GetDirectoryName(_configuration.GetValue<string>("FilePaths:SellersReportPath"));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var writer = new StreamWriter(directoryPath + fileName);
            writer.WriteLine("Sellers Report");
            writer.WriteLine("--------------------");
            writer.WriteLine();

            foreach (var record in sellers)
            {
                writer.WriteLine($"ID: {record.Id}");
                writer.WriteLine($"Name: {record.Name}");
                writer.WriteLine($"Email: {record.Email}");
                writer.WriteLine($"Birth Date: {record.BirthDate:dd/MM/yyyy}");
                writer.WriteLine($"Base Salary: {record.BaseSalary:F2}");
                writer.WriteLine($"Department ID: {record.DepartmentId}");
                writer.WriteLine("--------------------");
            }
        }
    }
}
