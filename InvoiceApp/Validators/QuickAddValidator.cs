﻿using InvoiceApp.Models;
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
            if (BrowserFile.Name == null || BrowserFile.Name == "")
            {
                ErrorMessages.Add("Select an image or document to be uploaded.");
                isValid = false;
            }
            if (BrowserFile.Size > 20971520)
            {
                ErrorMessages.Add("File size must be less than 20MB");
                isValid = false;
            }

            IsValid = isValid;
            return isValid;
        }

        public void ClearErrors()
        {
            IsValid = true;
            ErrorMessages.Clear();
        }
    }
}
