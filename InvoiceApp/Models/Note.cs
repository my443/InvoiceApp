using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public Employee WrittenBy { get; set; }
        public string NoteText { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
