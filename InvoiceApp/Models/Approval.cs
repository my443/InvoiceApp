namespace InvoiceApp.Models
{
    public class Approval
    {
        public int Id { get; set; }
        public Employee Approver { get; set; }
        //public List<Note> ApprovalComments { get; set; }
        public DateTime? LastUpdated { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }
        public int ExpenseId { get; set; }
    }
}
