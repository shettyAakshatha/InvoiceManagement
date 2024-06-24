using InvoiceManagement.Model.ViewModel;

namespace InvoiceManagement.InvoiceRepository
{
    public interface IInvoiceRepository
    {
        public InvoiceDetailsDataModel SaveInvoiceDetails(InvoiceDetailsDataModel invoiceDetailsData);
        public int UpdatePayments(InvoiceDetailsDataModel detailsDataModel);
        public  IEnumerable<InvoiceDetailsDataModel> GetAllInvoices(int filterStatusUnPaid = 0,int filterOverdue = 0);
        public int GetTopInvoiceID();
        public InvoiceDetailsDataModel GetInvoiceDetails(int invoiceID);
    }
}
