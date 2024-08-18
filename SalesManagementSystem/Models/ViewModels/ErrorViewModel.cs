namespace SalesManagementSystem.Models.ViewModels
{
    public record ErrorViewModel(string RequestId, string Message)
    {
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
