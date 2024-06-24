
using InvoiceManagement.Model;
using InvoiceManagement.Model.ViewModel;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace InvoiceManagement.InvoiceRepository
{
    public class InvoiceRepository : IInvoiceRepository
    {
       // public string InvoiceDataPath = "../InvoiceManagement/Data/Invoices";
       // public string MasterDataPath = "../InvoiceManagement/Data/MasterData/Master.json";
        private readonly ILogger<InvoiceRepository> _logger;
        private  AppSettings _appSettings;
        public InvoiceRepository(ILogger<InvoiceRepository> logger, IOptions<AppSettings> mySettings)
        {
            _logger = logger;
            _appSettings = mySettings.Value;
        }

        public InvoiceDetailsDataModel SaveInvoiceDetails(InvoiceDetailsDataModel invoiceDetailsData)
        {
            try
            {


                int response = UpdateJsonContent(invoiceDetailsData);
                if (response == 1)
                {
                    UpdateInvoiceID(invoiceDetailsData.id);
                    return invoiceDetailsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                throw;
            }


        }
        public int UpdatePayments(InvoiceDetailsDataModel detailsDataModel)
        {
            try
            {


                return UpdateJsonContent(detailsDataModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                throw;
            }
        }
        public InvoiceDetailsDataModel GetInvoiceDetails(int invoiceID)
        {
            try
            {
                if (File.Exists(_appSettings.InvoicePath + "/" + invoiceID + ".json"))
                {
                    string jsonData = File.ReadAllText(_appSettings.InvoicePath + "/" + invoiceID + ".json");
                    return JsonSerializer.Deserialize<InvoiceDetailsDataModel>(jsonData);

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                throw;
            }
        }
        
        public int UpdateJsonContent(InvoiceDetailsDataModel invoiceDetailsData)
        {
            try
            {
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(invoiceDetailsData);
                string fileName = invoiceDetailsData.id + ".json";
                File.WriteAllText(_appSettings.InvoicePath + "/" + fileName, jsonContent);
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                throw;
            }

        }
        public IEnumerable<InvoiceDetailsDataModel> GetAllInvoices(int filterStatusUnPaid = 0, int filterOverdue =0)
        {
            try
            {
                List<InvoiceDetailsDataModel> invoiceDetailsArray = new List<InvoiceDetailsDataModel>();
                foreach (string invoice in Directory.GetFiles(_appSettings.InvoicePath))
                {
                    InvoiceDetailsDataModel invoiceDetails = new InvoiceDetailsDataModel();
                    invoiceDetails = JsonSerializer.Deserialize<InvoiceDetailsDataModel>(File.ReadAllText(invoice));
                    if (filterStatusUnPaid == 1)
                    {
                        if (invoiceDetails.status == InvoiceStatus.Pending)
                        {
                            invoiceDetailsArray.Add(invoiceDetails);
                        }
                    }

                    if (filterOverdue == 1)
                    {
                        if (invoiceDetails.due_date < DateTime.Now)
                        {
                            invoiceDetailsArray.Add(invoiceDetails);
                        }
                    }
                    else
                    {
                        invoiceDetailsArray.Add(invoiceDetails);
                    }
                    //invoiceDetailsArray.Add(JsonSerializer.Deserialize <InvoiceDetailsDataModel > (File.ReadAllText(invoice)));

                }
                return invoiceDetailsArray;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                throw;
            }
        }
        public int GetTopInvoiceID()
        {
            try
            {
                string jsonData = File.ReadAllText(_appSettings.MasterdataPath);

                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonData);
                return jsonObject["invoiceId"];
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                throw;
            }
        }
        public int UpdateInvoiceID(int invoiceId)
        {
            try
            {
                string jsonData = File.ReadAllText(_appSettings.MasterdataPath);

                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonData);
                jsonObject["invoiceId"] = invoiceId;
                File.WriteAllText(_appSettings.MasterdataPath, System.Text.Json.JsonSerializer.Serialize(jsonObject));
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception :" + ex.Message);
                return 0;
            }
        }
    }
}
