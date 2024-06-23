using InvoiceManagement.AutoMapper;
using InvoiceManagement.InvoiceRepository;
using InvoiceManagement.Model;
using InvoiceManagement.Model.ViewModel;

namespace InvoiceManagement.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        public IInvoiceRepository _invoiceRepo { get; set; }
        public IAutomapper _mapper { get; set; }
        public int? nextInvoiceId { get; set; } 
        public InvoiceService( IInvoiceRepository invoiceRepository,IAutomapper automapper)
        {
            _invoiceRepo = invoiceRepository;
            _mapper = automapper;
        }
        public InvoiceDetailsViewModel SaveInvoiceDetails(CreateInvoiceViewModel invoiceDetails)
        {
            int invoiceID = (int)GetNextInvoiceId() ;
            InvoiceDetailsDataModel model = new InvoiceDetailsDataModel()
            {
                amount = invoiceDetails.amount,
                due_date = DateTime.Parse(invoiceDetails.due_date),
                status = InvoiceStatus.Pending,
                id = invoiceID
            };

            int status = _invoiceRepo.SaveInvoiceDetails(model);
            if (status == 1)
            {
                return new InvoiceDetailsViewModel
                {
                    id = invoiceID,
                    amount = invoiceDetails.amount,
                    paid_amount = 0,
                    due_date = DateTime.Parse(invoiceDetails.due_date),
                    status = Model.InvoiceStatus.Pending
                };
            }
            return null;
        }
        public IEnumerable<InvoiceDetailsViewModel> GetAllInvoiceDetails()
        {
            List<InvoiceDetailsDataModel> list = _invoiceRepo.GetAllInvoices().ToList();
           //pending mapping here

            List<InvoiceDetailsViewModel> list2 = new List<InvoiceDetailsViewModel>();
            return list2;
        }
        public int MakePayments(int invoiceID,float amount)
        {
            return _invoiceRepo.UpdatePayments(invoiceID, amount);
        }
        public int? GetNextInvoiceId()
        {
            nextInvoiceId = _invoiceRepo.GetMasterData() + 1;
            return nextInvoiceId;
        }


    }
}
