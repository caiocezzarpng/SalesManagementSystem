namespace SalesManagementSystem.Services.Factory.Interface
{
    public interface IReport<T>
    {
        void Generate(List<T> reports);
    }
}