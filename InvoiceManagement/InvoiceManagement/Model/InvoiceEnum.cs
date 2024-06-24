using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Model
{
    public enum InvoiceStatus
    {
        [Display(Name ="pending")]
        Pending,
        [Display(Name = "paid")]
        Paid,
        [Display(Name = "void")]
        Void

    }
    public class InvoiceEnum
    {
    }
}
