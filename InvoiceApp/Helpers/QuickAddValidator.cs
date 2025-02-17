using InvoiceApp.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace InvoiceApp.Validators
{
    public class QuickAddValidator
    {
        public List<String> ErrorMessages { get; set; }
        public bool IsValid = true;


        public QuickAddValidator()
        {
            ErrorMessages = new List<string>();
        }

        public bool Validate(Expense NewExpense, IBrowserFile BrowserFile)
        {
            bool isValid = true;

            if (NewExpense.TotalAmount <= 0)
            {
                ErrorMessages.Add("Total Amount must be greater than 0");
                isValid = false;
            }
            if (NewExpense.Vendor == null || NewExpense.Vendor == "")
            {
                ErrorMessages.Add("Enter a vendor name, or \"Unknown\"");
                isValid = false;
            }

            isValid = isValid ? CheckFileValidity(BrowserFile) : isValid;   // If isValid is false, return whatever comes from CheckFileValidity. Otherwise return false. 

            IsValid = isValid;
            return isValid;
        }

        private bool CheckFileValidity(IBrowserFile BrowserFile)
        {
            bool isValid = true;

            if (BrowserFile == null || BrowserFile.Name == null || BrowserFile.Name == "")
            {
                ErrorMessages.Add("Select an image or document to be uploaded.");
                isValid = false;
            }
            if (BrowserFile != null && BrowserFile.Size > 20971520)
            {
                ErrorMessages.Add("File size must be less than 20MB");
                isValid = false;
            }

            return isValid;
        }

        public void ClearErrors()
        {
            IsValid = true;
            ErrorMessages.Clear();
        }
    }
}
