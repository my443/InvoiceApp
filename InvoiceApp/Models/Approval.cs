namespace InvoiceApp.Models
{
    public class Approval
    {
        public int Id { get; set; }
        public Employee Approver { get; set; }
        //public List<Note> ApprovalComments { get; set; }
        public DateTime DateApproved { get; set; }
        public bool IsApproved { get; set; }
    }
}
