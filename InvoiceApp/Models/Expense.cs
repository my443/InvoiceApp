using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        [Precision(18, 2)]
        public decimal TotalHst { get; set; }
        public string Vendor { get; set; }
        public Employee SubmittedBy { get; set; }
        public List<Approval> Approvals { get; set; } = new List<Approval>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<ExpenseDetail> ExpenseDetails { get; set; } = new List<ExpenseDetail>();
        public List<Image> Images { get; set; } = new List<Image>();
        public ExpenseStatus ExpenseStatus { get; set; }
        public bool IsDeleted { get; set; }

    }
}
