using InvoiceApp.Data;
using InvoiceApp.Models;
using Microsoft.JSInterop;

namespace InvoiceApp.Components.Pages.ExpensePages
{
    public partial class Index
    {
        private ApplicationDbContext context = default!;
        private int selectedExpenseForDeletion;
        private List<Expense> Expenses = new List<Expense>();
        private Expense NewExpense = new Expense();

        protected override void OnInitialized()
        {
            context = DbFactory.CreateDbContext();
            LoadExpensesList();
        }

        private void LoadExpensesList()
        {
            Expenses = context.Expenses.Where(e => e.IsDeleted == false).ToList();
        }

        private void OpenDeleteModal(int id)
        {
            selectedExpenseForDeletion = id;
        }

        private async Task ConfirmDeleteExpense()
        {
            //Console.WriteLine("Deleting expense with id: " + selectedExpenseForDeletion);
            
            var expense = await context.Expenses.FindAsync(selectedExpenseForDeletion);

            if (expense != null)
            {
                expense.IsDeleted = true;                
                await context.SaveChangesAsync();
            }

            LoadExpensesList();
            
            // Hide the modal
            var modal = await JSRuntime.InvokeAsync<IJSObjectReference>("bootstrap.Modal.getInstance", "#deleteInvoiceModal");
            await modal.InvokeVoidAsync("hide");
        }

        public async ValueTask DisposeAsync() => await context.DisposeAsync();
    }
}
