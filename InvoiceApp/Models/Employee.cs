using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EE_ID {  get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int ApprovalLimit { get; set; }
        public bool IsProcessor {  get; set; }
    }
}
