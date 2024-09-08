using SalesManagementSystem.Services.Factory.Interface;

namespace SalesManagementSystem.Services.Factory
{
    public abstract class ReportFactory<T>
    {
        public abstract IReport<T> CreateReport();
    }
}