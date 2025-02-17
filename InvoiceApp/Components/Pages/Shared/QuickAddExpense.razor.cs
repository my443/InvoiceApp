using InvoiceApp.Data;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using InvoiceApp.Validators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace InvoiceApp.Components.Pages.Shared
{
    public partial class QuickAddExpense
    {
        [Parameter]
        public EventCallback OnClose { get; set; }

        private bool uploadSuccess = false;
        private bool uploadError = false;
        private InputFile inputFileElement;

        IBrowserFile BrowserFile;

        // For Approvers
        private string employeeSearchTerm = string.Empty;
        private int selectedEmployeeId;

        private string NoteText = string.Empty;

        private List<Employee> filteredEmployees = new List<Employee>();

        private List<Department> _departments;
        private List<GLAccount> _glaccounts;
        private ExpenseDetail NewExpenseDetail;

        private QuickAddValidator validator = new QuickAddValidator();

        int MaxFileSize = 20971520; // 20MB

        private async Task CloseModal()
        {
            OnClose.InvokeAsync();
            await JSRuntime.InvokeVoidAsync("eval", "bootstrap.Modal.getInstance(document.getElementById('addExpenseModal')).hide()");
        }

        private ApplicationDbContext context = default!;
        private Expense NewExpense = new Expense();


        protected override void OnInitialized()
        {
            context = DbFactory.CreateDbContext();
            GenerateNewExpense();

            filteredEmployees = context.Employees.ToList();
            _departments = context.Departments.ToList();
            _glaccounts = context.GLAccounts.ToList();
            ResetExpenseDetail();
        }

        private async Task AddNewExpense()
        {
            validator.ClearErrors();
            bool isValid = validator.Validate(NewExpense, BrowserFile);

            if (!isValid)
            {
                return;
            }

            NewExpense = SelectStatus(NewExpense);

            context.Expenses.Add(NewExpense);
            await context.SaveChangesAsync();
            int newExpenseId = NewExpense.Id;

            if (BrowserFile != null)
            {
                await UploadFileAsync(BrowserFile);
            }

            // Make the supporting records (eg approvals, Notes, ExpenseDetails)
            GenerateApproval(newExpenseId);
            GenerateNote(newExpenseId);
            GenerateExpenseDetail(newExpenseId);

            //CloseModal();
            //Cancel();           // Reset all variables.
            NavigationManager.NavigateTo($"/expenses/edit?id={newExpenseId}");
        }

        // Sets the status to A/P Processing if there are no approvers. 
        private Expense SelectStatus(Expense newExpense)
        {
            if (newExpense.ExpenseStatus.Id == 2)
            {
                if (!context.Approvals.Any(a => a.ExpenseId == NewExpense.Id))
                {
                    newExpense.ExpenseStatus = context.ExpenseStatus.Where(e => e.Id == 3).FirstOrDefault();    // Set to 'Approved For Processing'
                }
            }

            return newExpense;
        }

        private void SaveAndEdit()
        {
            validator.ClearErrors();
            bool isValid = validator.Validate(NewExpense, BrowserFile);

            if (!isValid)
            {
                return;
            }

            NewExpense.ExpenseStatus = context.ExpenseStatus.Where(e => e.Id == 1).FirstOrDefault();    // Set to 'Editing Mode'
            AddNewExpense();
            CloseModal();
            Cancel();           // Reset all variables.
        }

        private void GenerateNewExpense()
        {
            NewExpense = new Expense();
            NewExpense.IsDeleted = false;
            ExpenseStatus status = context.ExpenseStatus.Where(e => e.Id == 2).FirstOrDefault();    // By Default it is set to 'Requires Approval'
            Employee employee = context.Employees.Where(e => e.Id == 2).FirstOrDefault();           // TODO - Must remove this hardcoded value
            NewExpense.IsDeleted = false;
            NewExpense.ExpenseStatus = status;
            NewExpense.SubmittedBy = employee;
            NewExpense.Vendor = string.Empty;
        }

        private void GenerateNewBrowserFile()
        {
            BrowserFile = null;
        }

        private void ResetSelectedApprover()
        {
            selectedEmployeeId = 0;
        }

        private void Cancel()
        {
            validator.ClearErrors();
            GenerateNewExpense();
            GenerateNewBrowserFile();
            ResetSelectedApprover();
            ResetExpenseDetail();
        }

        // File Upload Section
        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            BrowserFile = e.File;
        }

        private int SaveFilenameRecordToDatabase(IBrowserFile file)
        {
            // Get the file extension
            string fileExtension = Path.GetExtension(file.Name);
            FileType fileType = GetFileType(fileExtension);

            // Save the image record to the database
            var image = new Image
            {
                FileName = file.Name,
                UploadTime = DateTime.Now,
                UploadedBy = NewExpense.SubmittedBy,
                ExpenseId = NewExpense.Id,
                FileType = fileType,
            };

            context.Images.Add(image);
            context.SaveChanges();   // Save the filename to the database
                                     // RefreshAttachmentsList();
            return image.Id;
        }

        // Returns FileType or default to 'Other'
        private FileType GetFileType(string fileExtension)
        {
            FileType fileType = context.FileTypes.FirstOrDefault(ft => ft.Extension == fileExtension);

            if (fileType is null)
            {
                fileType = context.FileTypes.FirstOrDefault(ft => ft.Id == -1); // Default to 'Other'
            }

            return fileType;
        }

        private async Task UploadFileAsync(IBrowserFile file)
        {
            int imageDbID = SaveFilenameRecordToDatabase(file);
            string filename = $"{imageDbID.ToString()}-{file.Name}";

            var uploadsFolder = Path.Combine(WebHostEnvironment.ContentRootPath, "SecureUploads");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, filename);

            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: MaxFileSize).CopyToAsync(fileStream);     // Change the maxAllowedSize here.
        }

        private void GenerateApproval(int newExpenseId)
        {
            if (selectedEmployeeId != null && selectedEmployeeId != 0)
            {
                Approval newApproval = new Approval();
                newApproval.Approver = context.Employees.Where(e => e.Id == selectedEmployeeId).FirstOrDefault();
                newApproval.ExpenseId = newExpenseId;
                newApproval.IsApproved = false;
                context.Approvals.Add(newApproval);
                context.SaveChanges();
            }
        }

        private void GenerateNote(int newExpenseId)
        {
            if (!string.IsNullOrEmpty(NoteText))
            {
                Note newNote = new Note();
                newNote.ExpenseId = newExpenseId;
                newNote.NoteText = NoteText;
                newNote.WrittenBy = context.Employees.Where(e => e.Id == 2).FirstOrDefault();           // TODO - Must remove this hardcoded value
                newNote.DateAdded = DateTime.Now;
                context.Notes.Add(newNote);
                context.SaveChanges();
            }
        }

        private void GenerateExpenseDetail(int NewExpenseId)
        {
            //NewExpenseDetail = new ExpenseDetail();
            Expense expense = context.Expenses.Where(e => e.Id == NewExpenseId).FirstOrDefault();
            NewExpenseDetail.ExpenseId = NewExpenseId;
            NewExpenseDetail.Amount = expense.TotalAmount;
            NewExpenseDetail.Hst = expense.TotalHst;

            context.Add(NewExpenseDetail);
            context.SaveChanges();
        }

        private void ResetExpenseDetail()
        {
            NewExpenseDetail = new ExpenseDetail { AccountId = -1, DepartmentId = -1 };
        }
    }
}
