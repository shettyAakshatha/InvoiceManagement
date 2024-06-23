namespace InvoiceManagement.Model.ViewModel
{
    public class InvoiceDetailsViewModel
    {
        public int id { get; set; }
        public float amount { get; set; }
        public float paid_amount { get; set; }
        public DateTime due_date { get; set; }
        public InvoiceStatus status { get; set; }
    }
}
