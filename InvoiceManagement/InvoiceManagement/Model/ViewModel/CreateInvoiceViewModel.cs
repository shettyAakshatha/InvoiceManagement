using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Model.ViewModel
{
    public class CreateInvoiceViewModel
    {
        [Required]
        public float amount { get; set; }
        [Required]
        public string due_date { get; set; }
    }
}
