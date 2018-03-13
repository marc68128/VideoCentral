namespace VideosCentral.CameraConfigurator.Services.Contracts
{
    public interface IWindowService
    {
        void ShowWindow(object viewModel, string windowId);
        void HideWindow(string windowId);
    }
}