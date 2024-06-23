using InvoiceManagement.Model.ViewModel;

namespace InvoiceManagement.InvoiceRepository
{
    public interface IInvoiceRepository
    {
        public int SaveInvoiceDetails(InvoiceDetailsDataModel invoiceDetailsData);
        public int UpdatePayments(int invoiceID, float amountPaid);
        public  IEnumerable<InvoiceDetailsDataModel> GetAllInvoices();
        public int GetMasterData();
    }
}
