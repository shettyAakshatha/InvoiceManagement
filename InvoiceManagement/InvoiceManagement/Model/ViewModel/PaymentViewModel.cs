using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Model.ViewModel
{
    public class PaymentViewModel
    {
        [Required]
        public float amount { get; set; }
    }
}
