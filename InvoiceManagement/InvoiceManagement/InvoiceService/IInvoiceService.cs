using AutoMapper;
using InvoiceManagement.InvoiceRepository;
using InvoiceManagement.Model.ViewModel;

namespace InvoiceManagement.InvoiceService
{
    public interface IInvoiceService
    {
        public IInvoiceRepository _invoiceRepo { get; set; }
        public IMapper _mapper { get; set; }
        public InvoiceDetailsViewModel SaveInvoiceDetails(CreateInvoiceViewModel invoiceDetails);
        public int MakePayments(int invoiceID, float amountPaid);
        public InvoiceDetailsViewModel GetInvoiceDetails(int invoiceID);
        public IEnumerable<InvoiceDetailsViewModel> GetAllInvoiceDetails(int filterStatusUnPaid = 0);
        public int ProcessOverDue(float lateFee, int overDuedays);
    }
}
