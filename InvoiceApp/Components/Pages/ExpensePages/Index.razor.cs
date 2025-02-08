using InvoiceApp.Data;

namespace InvoiceApp.Components.Pages.ExpensePages
{
    public partial class Index
    {
        private ApplicationDbContext context = default!;

        protected override void OnInitialized()
        {
            context = DbFactory.CreateDbContext();
        }

        public async ValueTask DisposeAsync() => await context.DisposeAsync();
    }
}
