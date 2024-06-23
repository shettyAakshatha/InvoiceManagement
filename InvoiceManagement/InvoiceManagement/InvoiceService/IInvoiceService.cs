using InvoiceManagement.Model.ViewModel;

namespace InvoiceManagement.InvoiceService
{
    public interface IInvoiceService
    {
        public InvoiceDetailsViewModel SaveInvoiceDetails(CreateInvoiceViewModel invoiceDetails);
        public int MakePayments(int invoiceID, float amountPaid);
        public IEnumerable<InvoiceDetailsViewModel> GetAllInvoiceDetails();
    }
}
