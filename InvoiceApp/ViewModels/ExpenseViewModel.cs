using InvoiceApp.Data;
using InvoiceApp.Models;
using InvoiceApp.Components.Icons;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;


namespace InvoiceApp.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        private readonly ApplicationDbContext _appDbContext;
        private bool UserCanApprove { get; set; }
        private bool UserCanProcess { get; set; }

        [Precision(18, 2)]
        public double TotalAmount { get; set; }
        [Precision(18, 2)]
        public double TotalHst { get; set; }
        public IQueryable<ExpenseDetail> ExpenseDetails { get; set; } = new List<ExpenseDetail>().AsQueryable();

        public Dictionary<string, bool> sectionVisibility = new Dictionary<string, bool>
            {
                { "ExpenseSummary", true },
                { "ExpenseDetails", true },
                { "Approvals", true },
                { "Images", true },
                { "Notes", true },
                { "NoteModal", false },
                { "AllSections", true },
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

        public int AddExpense(Expense expense)
        {
            _appDbContext.Expenses.Add(expense);
            _appDbContext.SaveChanges();
            return expense.Id;
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
        public IQueryable<ExpenseDetail> GetExpenseDetails(int expenseId)
        {
            List<ExpenseDetail> expenseDetails = _appDbContext.ExpenseDetails.Where(e => e.ExpenseId == expenseId).ToList();
            ExpenseDetails = expenseDetails.AsQueryable();

            return ExpenseDetails;
        }

        // Ensure that the sum of all ExpenseDetails is equal to the TotalAmount and TotalHst
        public void EnsureExpenseDetailsSum(Expense expense)
        {
            decimal currentDetailsAmountTotal = _appDbContext.ExpenseDetails.Sum(ed => ed.Amount);
            decimal currentDetailsHstTotal = _appDbContext.ExpenseDetails.Sum(ed => ed.Hst);


            Console.WriteLine($"Current Details Amount Total: {currentDetailsAmountTotal}");
            Console.WriteLine($"Current Details HST Total: {currentDetailsHstTotal}");

            if (currentDetailsAmountTotal != expense.TotalAmount || currentDetailsHstTotal != expense.TotalHst)
            {
                decimal amountDifference = expense.TotalAmount - currentDetailsAmountTotal;
                decimal hstDifference = expense.TotalHst - currentDetailsHstTotal;

                GLAccount glAccount = _appDbContext.GLAccounts.FirstOrDefault(a => a.Id == -1);
                Department department = _appDbContext.Departments.FirstOrDefault(d => d.Id == -1);

                // Add a new ExpenseDetail to balance the amounts
                ExpenseDetail balancingDetail = new ExpenseDetail
                {
                    Account = glAccount, // Set appropriate GLAccount
                    Department = department, // Set appropriate Department
                    Amount = amountDifference,
                    Hst = hstDifference,
                    ExpenseId = expense.Id
                };

                expense.ExpenseDetails.Add(balancingDetail);
                _appDbContext.SaveChanges();
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

        public RenderFragment SectionToggleIcon(string sectionName)
        {
            if (sectionVisibility.ContainsKey(sectionName))
            {
                return sectionVisibility[sectionName] ?
                    (RenderFragment)(builder => { builder.OpenComponent<AddIcon>(0); builder.CloseComponent(); })
                    : (builder => { builder.OpenComponent<SubtractIcon>(0); builder.CloseComponent(); });
            }
            //return "none";
            return new RenderFragment(builder => { });
        }

    }
}
