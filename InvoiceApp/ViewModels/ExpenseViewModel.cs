using InvoiceApp.Data;
using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        private readonly ApplicationDbContext _appDbContext;
        private bool UserCanApprove { get; set; }
        private bool UserCanProcess { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        [Precision(18, 2)]
        public decimal TotalHst { get; set; }

        public Dictionary<string, bool> sectionVisibility = new Dictionary<string, bool>
            {
                { "ExpenseSummary", true },
                { "ExpenseDetails", true }
            };

        public ExpenseViewModel(ApplicationDbContext appDbContext)
        {
            UserCanApprove = false;
            UserCanProcess = false;
            _appDbContext = appDbContext;
        }

        public bool GetCanApprove()
        {
            return UserCanApprove;
        }

        public void SetCanApprove(bool status)
        {
            UserCanApprove = status;
        }

        public bool GetCanProcess()
        {
            return UserCanProcess;
        }

        public void SetCanProcess(bool status)
        {
            UserCanProcess = status;
        }

        public void AddExpense(Expense expense)
        {
            _appDbContext.Expenses.Add(expense);
            _appDbContext.SaveChanges();
        }

        public Expense CreateExpense(Expense newExpense)
        {
            return newExpense;
        }

        // Read an expense by ID
        public Expense GetExpense(int id)
        {
            return _appDbContext.Expenses.FirstOrDefault(e => e.Id == id);
        }

        // Update an existing expense
        public void UpdateExpense(int id, decimal totalAmount, decimal totalHst, string vendor, Employee submittedBy, ExpenseStatus expenseStatus)
        {
            Expense expenseToUpdate = _appDbContext.Expenses.FirstOrDefault(e => e.Id == id);
            if (expenseToUpdate != null)
            {
                expenseToUpdate.TotalAmount = totalAmount;
                expenseToUpdate.TotalHst = totalHst;
                expenseToUpdate.Vendor = vendor;
                expenseToUpdate.SubmittedBy = submittedBy;
                expenseToUpdate.ExpenseStatus = expenseStatus;

                EnsureExpenseDetailsSum(expenseToUpdate);

                _appDbContext.SaveChanges();
            }
        }

        // Delete an expense by ID
        public void DeleteExpense(int id)
        {
            Expense expenseToDelete = _appDbContext.Expenses.FirstOrDefault(e => e.Id == id);
            if (expenseToDelete != null)
            {
                _appDbContext.Expenses.Remove(expenseToDelete);
                _appDbContext.SaveChanges();
            }
        }

        // Get the list of expenses
        public List<Expense> GetExpensesList()
        {
            return _appDbContext.Expenses.ToList();
        }

        // Get the list of expense details for a specific expense
        public List<ExpenseDetail> GetExpenseDetails(int expenseId)
        {
            Expense expense = _appDbContext.Expenses.FirstOrDefault(e => e.Id == expenseId);
            return expense?.ExpenseDetails ?? new List<ExpenseDetail>();
        }

        // Ensure that the sum of all ExpenseDetails is equal to the TotalAmount and TotalHst
        private void EnsureExpenseDetailsSum(Expense expense)
        {
            decimal currentTotalAmount = expense.ExpenseDetails.Sum(ed => ed.Amount);
            decimal currentTotalHst = expense.ExpenseDetails.Sum(ed => ed.Hst);

            if (currentTotalAmount != expense.TotalAmount || currentTotalHst != expense.TotalHst)
            {
                decimal amountDifference = expense.TotalAmount - currentTotalAmount;
                decimal hstDifference = expense.TotalHst - currentTotalHst;

                // Add a new ExpenseDetail to balance the amounts
                ExpenseDetail balancingDetail = new ExpenseDetail
                {
                    Account = null, // Set appropriate GLAccount
                    Department = null, // Set appropriate Department
                    Amount = amountDifference,
                    Hst = hstDifference
                };

                expense.ExpenseDetails.Add(balancingDetail);
            }
        }

        /// <summary>
        /// Toggles the visibility of a section. Based on the section name that is part of the dictionary.
        /// </summary>
        /// <param name="sectionName"></param>
        public void ToggleSection(string sectionName)
        {
            if (sectionVisibility.ContainsKey(sectionName))
            {
                sectionVisibility[sectionName] = !sectionVisibility[sectionName];
            }
        }
    }
}
