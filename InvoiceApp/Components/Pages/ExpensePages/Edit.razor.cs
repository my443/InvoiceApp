using InvoiceApp.Data;
using InvoiceApp.Models;
using InvoiceApp.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace InvoiceApp.Components.Pages.ExpensePages
{
    public partial class Edit
    {
        [SupplyParameterFromQuery]
        private int Id { get; set; }
        private Note newNote = new Note();

        private Employee? _employee { get; set; }
        private ExpenseViewModel _expenseViewModel;
        private ApplicationDbContext context;

        [SupplyParameterFromForm]
        private Expense? Expense { get; set; }


        private string employeeSearchTerm = string.Empty;
        private int selectedEmployeeId;
        private List<Employee> employees = new List<Employee>();
        private List<Employee> filteredEmployees = new List<Employee>();

        private List<Approval> _approvals = new List<Approval>();
        private List<Image> _images = new List<Image>();

        private EditForm editForm;

        private int selectedApprovalForDeletion;
        private int selectedAttachmentForDeletion;

        private List<Department> _departments;
        private List<GLAccount> _glaccounts;

        public bool IsEditing { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            context = DbFactory.CreateDbContext();
            _expenseViewModel = new ExpenseViewModel(context);

            Expense ??= await context.Expenses.Include(e => e.ExpenseStatus).FirstOrDefaultAsync(m => m.Id == Id);
            _expenseViewModel.EnsureExpenseDetailsSum(Expense);
            _expenseViewModel.GetExpenseDetails(Expense.Id);

            if (Expense is null)
            {
                NavigationManager.NavigateTo("notfound");
            }

            SetEditingMode();

            _employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
            if (_employee != null)
            {
                Expense.SubmittedBy = _employee;
            }

            employees = await context.Employees.ToListAsync();
            filteredEmployees = employees;

            RefreshAttachmentsList();

            _approvals = await context.Approvals.Where(approval => approval.ExpenseId == Expense.Id).ToListAsync();
            _departments = await context.Departments.ToListAsync();
            _glaccounts = await context.GLAccounts.ToListAsync();
        }

        private void SetEditingMode()
        {
            if (Expense.ExpenseStatus.Id == 1) // 1 is "Created"
            {
                IsEditing = true;
            }
            else
            {
                IsEditing = false;
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://learn.microsoft.com/aspnet/core/blazor/forms/#mitigate-overposting-attacks.
        private async Task UpdateExpense()
        {
            // using var context = DbFactory.CreateDbContext();
            context.Attach(Expense!).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(Expense!.Id))
                {
                    NavigationManager.NavigateTo("notfound");
                }
                else
                {
                    throw;
                }
            }
            // RefreshPage();
            // NavigationManager.NavigateTo("/expenses");
        }

        private bool ExpenseExists(int id)
        {
            // using var context = DbFactory.CreateDbContext();
            return context.Expenses.Any(e => e.Id == id);
        }

        private async Task AddNote()
        {
            int expenseId = Expense.Id;

            if (newNote.NoteText is null)
            {
                return;
            }

            newNote.NoteText = newNote.NoteText.Replace("\n", "<br/>").Replace("\r", "<br/>");
            newNote.WrittenBy = _employee;
            newNote.DateAdded = DateTime.Now;
            newNote.ExpenseId = expenseId;

            Console.WriteLine($"{newNote.NoteText}, {newNote.WrittenBy}");
            context.Notes.Add(newNote);
            await context.SaveChangesAsync();
            newNote = new Note(); // Reset the note

            _expenseViewModel.sectionVisibility["NoteModal"] = false; // Close the modal
        }

        private async Task AddApproval()
        {
            Employee approver = await context.Employees.FirstOrDefaultAsync(e => e.Id == selectedEmployeeId);

            if (approver is null)
            {
                return;
            }

            bool isDuplicate = context.Approvals.Any(a => a.Approver.Id == approver.Id && a.ExpenseId == Expense.Id);
            if (isDuplicate)
            {
                return;
            }

            var approval = new Approval
            {
                Approver = approver,
                LastUpdated = DateTime.Now,
                IsApproved = false,
                ExpenseId = Expense.Id
            };

            context.Approvals.Add(approval);
            await context.SaveChangesAsync();
        }

        private void FilterEmployees(ChangeEventArgs e)
        {
            employeeSearchTerm = e.Value.ToString();
            filteredEmployees = employees
                .Where(emp => emp.FullName.Contains(employeeSearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            IBrowserFile file = e.File;

            // Save to Database. Get Filename
            int imageDbID = SaveFilenameRecordToDatabase(file);
            string filename = $"{imageDbID.ToString()}-{file.Name}";

            var uploadsFolder = Path.Combine(WebHostEnvironment.ContentRootPath, "SecureUploads");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, filename);

            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fileStream);
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
                UploadedBy = _employee,
                ExpenseId = Expense.Id,
                FileType = fileType,
            };

            context.Images.Add(image);
            context.SaveChanges();   // Save the filename to the database
            RefreshAttachmentsList();
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

        private async Task DownloadFile(int imageId)
        {
            var image = await context.Images
                .Where(img => img.Id == imageId)
                .FirstOrDefaultAsync();

            string fileName = $"{image.Id}-{image.FileName}";
            Console.WriteLine(fileName);

            var filePath = Path.Combine(WebHostEnvironment.ContentRootPath, "SecureUploads", fileName);

            var fileBytes = await File.ReadAllBytesAsync(filePath);

            var base64 = Convert.ToBase64String(fileBytes);
            await JSRuntime.InvokeVoidAsync("openFileFromBase64", fileName, base64);
        }

        private async Task RefreshAttachmentsList()
        {
            _images = await context.Images
                        .Where(img => img.ExpenseId == Expense.Id)
                        .ToListAsync();
        }

        private async Task SubmitForm()
        {
            await UpdateExpense();
        }

        private void SaveAndRefreshPage()
        {
            UpdateExpense();
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }

        private void SaveAndReturnToList()
        {
            UpdateExpense();
            NavigationManager.NavigateTo("/expenses");
        }

        private void DeleteApprover(int approvalId)
        {
            selectedApprovalForDeletion = approvalId;
        }

        private async Task ConfirmDeleteApproval()
        {
            var approval = await context.Approvals.FindAsync(selectedApprovalForDeletion);
            if (approval != null)
            {
                context.Approvals.Remove(approval);
                await context.SaveChangesAsync();
            }

            // Hide the modal
            var modal = await JSRuntime.InvokeAsync<IJSObjectReference>("bootstrap.Modal.getInstance", "#deleteApprovalModal");
            await modal.InvokeVoidAsync("hide");
        }

        private void DeleteAttachment(int imageId)
        {
            selectedAttachmentForDeletion = imageId;
        }

        private async Task ConfirmDeleteAttachment()
        {

            var image = context.Images.Find(selectedAttachmentForDeletion);
            string fileName = $"{image.Id}-{image.FileName}";

            if (image != null)
            {
                context.Images.Remove(image);
                context.SaveChanges();
            }

            // Delete the file from the directory.
            File.Delete(Path.Combine(WebHostEnvironment.ContentRootPath, "SecureUploads", fileName));

            var modal = await JSRuntime.InvokeAsync<IJSObjectReference>("bootstrap.Modal.getInstance", "#deleteAttachmentModal");
            await modal.InvokeVoidAsync("hide");

            RefreshAttachmentsList();
            StateHasChanged();
        }

        private async Task OnDepartmentChanged(ExpenseDetail expensedetail)
        {
            context.Update(expensedetail);
            await context.SaveChangesAsync();
            _expenseViewModel.EnsureExpenseDetailsSum(Expense);
            // ToDo - Update the correct amount for the whole list of transactions.
            StateHasChanged();
        }

        private async Task DeleteDetailRow(ExpenseDetail expensedetail)
        {
            var previousExpenseDetail = context.ExpenseDetails
                .Where(ed => ed.ExpenseId == expensedetail.ExpenseId && ed.Id < expensedetail.Id)
                .OrderByDescending(ed => ed.Id)
                .FirstOrDefault();

            if (previousExpenseDetail != null)
            {
                previousExpenseDetail.Amount += expensedetail.Amount;
                previousExpenseDetail.Hst += expensedetail.Hst;
            }

            context.ExpenseDetails.Remove(expensedetail);

            await context.SaveChangesAsync();
            _expenseViewModel.EnsureExpenseDetailsSum(Expense);
            StateHasChanged();
        }

        /// <summary>
        /// Look at the last expense, and the divide it in 1/2.
        /// Then let the app update the next row to balance out the amount that it needs to be.
        /// (EnsureExpenseDetailsSum)
        /// </summary>
        /// <returns></returns>
        private async Task AddExpenseDetail()
        {

            var lastExpenseDetail = context.ExpenseDetails
                .Where(ed => ed.ExpenseId == Expense.Id)
                .OrderByDescending(ed => ed.Id)
                .FirstOrDefault();

            if (lastExpenseDetail != null)
            {
                lastExpenseDetail.Amount = Math.Round(lastExpenseDetail.Amount / 2, 2);
                lastExpenseDetail.Hst = Math.Round(lastExpenseDetail.Hst / 2, 2);
            }

            await context.SaveChangesAsync();

            _expenseViewModel.EnsureExpenseDetailsSum(Expense);
            StateHasChanged();
        }

        private async Task Approve(int approvalId)
        {
            var approval = await context.Approvals.FindAsync(approvalId);
            if (approval != null)
            {
                approval.IsApproved = true;
                await context.SaveChangesAsync();
            }
        }

        public void ToggleIsEditing()
        {
            IsEditing = !IsEditing;
            
            
            if(!IsEditing)
            {
                Expense.ExpenseStatus = context.ExpenseStatus.FirstOrDefault(es => es.Id == 2);         // 2 is "Requires Approval"

            }
            context.SaveChanges();
        }
    }
}
