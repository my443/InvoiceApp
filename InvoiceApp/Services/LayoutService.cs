namespace InvoiceApp.Services
{
    public class LayoutService
    {
        public int sidebarWidth { get; set; } = 270;

        public event Action OnSidebarWidthChanged; // Add an event

        public void SetSidebarWidth(int width)
        {
            sidebarWidth = width;
            OnSidebarWidthChanged?.Invoke(); // Trigger the event
        }
    }
}
