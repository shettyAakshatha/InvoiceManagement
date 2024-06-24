using AutoMapper;
using InvoiceManagement.AutoMapper;
using InvoiceManagement.Controllers;
using InvoiceManagement.InvoiceRepository;
using InvoiceManagement.Model;
using InvoiceManagement.Model.ViewModel;

namespace InvoiceManagement.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        public IInvoiceRepository _invoiceRepo { get; set; }
        public IMapper _mapper { get; set; }
        private readonly ILogger<InvoiceService> _logger;
        public int? nextInvoiceId { get; set; }
        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper,ILogger<InvoiceService> logger)
        {
            _invoiceRepo = invoiceRepository;            
            _mapper = mapper;
            _logger = logger;
        }
        
        public InvoiceDetailsViewModel SaveInvoiceDetails(CreateInvoiceViewModel invoiceDetails)
        {
            try
            {
                int invoiceID = (int)GetNextInvoiceId();
                InvoiceDetailsDataModel model = new InvoiceDetailsDataModel()
                {
                    amount = invoiceDetails.amount,
                    due_date = DateTime.Parse(invoiceDetails.due_date),
                    status = InvoiceStatus.Pending,
                    id = invoiceID
                };

                var responseModel = _invoiceRepo.SaveInvoiceDetails(model);
                if (responseModel != null)
                {
                    return _mapper.Map<InvoiceDetailsDataModel, InvoiceDetailsViewModel>(responseModel);
                    
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {  
                _logger.LogError(" Exception:"+ ex.Message);
                throw;
            }
        }
        public IEnumerable<InvoiceDetailsViewModel> GetAllInvoiceDetails(int filterStatusUnPaid = 0)
        {
            try
            {
                IEnumerable<InvoiceDetailsDataModel> datalist = _invoiceRepo.GetAllInvoices().ToList();
                //pending mapping here

                IEnumerable<InvoiceDetailsViewModel> viewlist = _mapper.Map<IEnumerable<InvoiceDetailsViewModel>>(datalist);
                return viewlist;
            }
            catch (Exception ex)
            {
                _logger.LogError(" Exception:" + ex.Message);
                throw;
            }
        }

        public InvoiceDetailsViewModel GetInvoiceDetails(int invoiceID)
        {
            try
            {

                InvoiceDetailsDataModel invoiceDetails = _invoiceRepo.GetInvoiceDetails(invoiceID);
                InvoiceDetailsViewModel model = _mapper.Map<InvoiceDetailsDataModel, InvoiceDetailsViewModel>(invoiceDetails);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(" Exception:" + ex.Message);
                throw;
            }

        }


        public int MakePayments(int invoiceID,float amount)
        {
            try
            {
                InvoiceDetailsViewModel invoiceDetails = GetInvoiceDetails(invoiceID);
                if (invoiceDetails != null)
                {
                    invoiceDetails.paid_amount += amount;
                    if (invoiceDetails.amount <= invoiceDetails.paid_amount)
                    {
                        invoiceDetails.status = InvoiceStatus.Paid;

                    }

                    return _invoiceRepo.UpdatePayments(_mapper.Map<InvoiceDetailsViewModel, InvoiceDetailsDataModel>(invoiceDetails));
                }
                else
                {

                    return 2;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" Exception:" + ex.Message);
                throw;
            }
        }
        public int? GetNextInvoiceId()
        {
            nextInvoiceId = _invoiceRepo.GetTopInvoiceID() + 1;
            return nextInvoiceId;
        }
        public int ProcessOverDue(float lateFee, int overDuedays)
        {
            try
            {
                IEnumerable<InvoiceDetailsDataModel> unPaidInvoices = _invoiceRepo.GetAllInvoices(1, 1);
                InvoiceDetailsDataModel invoiceDetails = new InvoiceDetailsDataModel();
                foreach (var invoice in unPaidInvoices)
                {
                    if (invoice.status == Model.InvoiceStatus.Pending && (invoice.due_date.AddDays(overDuedays) < DateTime.Now))
                    {
                        invoiceDetails.id = (int)GetNextInvoiceId();
                        invoiceDetails.status = InvoiceStatus.Pending;
                        invoiceDetails.due_date = DateTime.Now.AddDays(30);
                        if (invoice.paid_amount > 0)
                        {
                            //update existing once, create a new one
                            invoice.status = Model.InvoiceStatus.Paid;
                            int response = _invoiceRepo.UpdatePayments(invoice);
                            if (response > 0)
                            {
                                invoiceDetails.amount = (invoice.amount - invoice.paid_amount) + lateFee;


                            }

                        }
                        else
                        {
                            //update exisitng one as void, create a new one
                            invoice.status = Model.InvoiceStatus.Void;
                            int response = _invoiceRepo.UpdatePayments(invoice);
                            if (response > 0)
                            {
                                invoiceDetails.amount = invoice.amount + lateFee;


                            }

                        }
                        _invoiceRepo.SaveInvoiceDetails(invoiceDetails);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(" Exception:" + ex.Message);
                throw;
            }
        }


    }
}
