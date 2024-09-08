using SalesManagementSystem.Models;
using SalesManagementSystem.Services.Factory.Interface;

namespace SalesManagementSystem.Services.Reports
{
    public class SaleReport : IReport<SalesRecord>
    {
        private readonly IConfiguration _configuration;

        public SaleReport(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Generate(List<SalesRecord> salesRecords)
        {
            var fileName = $"\\report-{DateTime.Now:yyyyMMddHHmmss}.txt";

            var directoryPath = Path.GetDirectoryName(_configuration.GetValue<string>("FilePaths:SalesReportPath"));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var writer = new StreamWriter(directoryPath + fileName);
            {
                writer.WriteLine("Sales Report");
                writer.WriteLine("--------------------");
                writer.WriteLine();

                foreach (var record in salesRecords)
                {
                    writer.WriteLine($"ID: {record.Id}");
                    writer.WriteLine($"Date: {record.Date:dd/MM/yyyy}");
                    writer.WriteLine($"Amount: {record.Amount:F2}");
                    writer.WriteLine($"Status: {record.Status}");
                    writer.WriteLine($"Seller: {record.Seller.Name}");
                    writer.WriteLine("--------------------");
                }
            }
        }
    }
}