using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationType { get; set; }
        public string NotificationMessage { get; set; }
        public List<Employee> Reciptients { get; set; }
        public DateTime NotificationDateTime { get; set; }
    }
}
