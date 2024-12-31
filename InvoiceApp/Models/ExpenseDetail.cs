using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models
{
    public class ExpenseDetail
    {
        public int Id { get; set; }
        public GLAccount Account { get; set; }
        public Department Department { get; set; }

        [Precision(18, 2)]
        public decimal Hst {  get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        // For FK relationship
        public int ExpenseId { get; set; }
    }
}
