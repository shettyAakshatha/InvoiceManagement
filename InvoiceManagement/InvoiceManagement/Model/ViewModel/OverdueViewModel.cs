using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Model.ViewModel
{
    public class OverdueViewModel
    {
        [Required]
        public float late_fee { get; set; }
        [Required] 
        public int overdue_days { get; set; }
    }
}
