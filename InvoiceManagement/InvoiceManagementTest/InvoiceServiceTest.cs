using AutoMapper;
using Castle.Core.Logging;
using InvoiceManagement.InvoiceRepository;
using InvoiceManagement.InvoiceService;
using InvoiceManagement.Model.ViewModel;
using Microsoft.Extensions.Logging;
using Moq;

namespace InvoiceManagementTest
{
    public class Tests
    {
        public Mock<IInvoiceRepository> mock_InvoiceRepository = new Mock<IInvoiceRepository>();
        public Mock<IMapper> mock_Mapper = new Mock<IMapper>();
        public Mock<ILogger<InvoiceService>> mock_Logger = new Mock<ILogger<InvoiceService>>();
        public IInvoiceService InvoiceService { get; set; }

        [SetUp]
        public void Setup()
        {
            //var mock_InvoiceRepository = new Mock<IInvoiceRepository>();
            mock_Mapper.Setup(x => x.Map<InvoiceDetailsDataModel,InvoiceDetailsViewModel>(It.IsAny<InvoiceDetailsDataModel>())).Returns(new InvoiceDetailsViewModel {
                id = 1003,
                amount = 123,
                paid_amount = 0,
                due_date = DateTime.Now.AddMonths(1),
                status = InvoiceManagement.Model.InvoiceStatus.Pending
            });
            InvoiceService = new InvoiceService(mock_InvoiceRepository.Object, mock_Mapper.Object, (Microsoft.Extensions.Logging.ILogger<InvoiceService>)mock_Logger.Object);
        }

        [Test]
        public void SaveInvoiceDetails_OnSuccess()
        {
            Setup();
            var InvoiceDataModel = new InvoiceDetailsDataModel()
            {
                id = 1003,
                amount = 123,
                paid_amount = 0,
                due_date = DateTime.Now.AddMonths(1),
                status = InvoiceManagement.Model.InvoiceStatus.Pending

            };
            List<InvoiceDetailsDataModel> invoiceDetailsDataModels = new List<InvoiceDetailsDataModel>();
            invoiceDetailsDataModels.Add(InvoiceDataModel);
            mock_InvoiceRepository.Setup(x => x.SaveInvoiceDetails(It.IsAny<InvoiceDetailsDataModel>())).Returns(InvoiceDataModel);

            mock_InvoiceRepository.Setup(x => x.GetAllInvoices(0, 0)).Returns(invoiceDetailsDataModels);

            mock_InvoiceRepository.Setup(x => x.GetInvoiceDetails(It.IsAny<int>())).Returns(InvoiceDataModel);

            var response = InvoiceService.SaveInvoiceDetails(new CreateInvoiceViewModel()
            {
                amount = 123,
                due_date = "2024-07-24"

            });
            Assert.Equals(InvoiceDataModel.amount, response.amount);
            Assert.Equals(InvoiceDataModel.status, response.status);

        }
        [Test] public void SaveInvoiceDetails_OnFailure()
        {
            Setup();
            string errorMsg = "Error from repository layer";
            Exception repoException = new Exception(errorMsg);
           // mock_InvoiceRepository.Setup(x => x.SaveInvoiceDetails(It.IsAny<InvoiceDetailsDataModel>())).Returns(repoException);

        }
        [Test]
        public void GetAllInvoiceDetails_OnSucess()
        {
            Setup();
        }
    }
}