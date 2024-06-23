using InvoiceManagement.Model;
using InvoiceManagement.Model.ViewModel;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace InvoiceManagement.InvoiceRepository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public string InvoiceDataPath = "../InvoiceManagement/Data/Invoices";
        public string MasterDataPath = "../InvoiceManagement/Data/MasterData/Master.json";
        public int SaveInvoiceDetails(InvoiceDetailsDataModel invoiceDetailsData)
        {

            return GetJsonContent(invoiceDetailsData);

        }
        public int UpdatePayments(int invoiceID, float amountPaid)
        {
            string jsonData = File.ReadAllText(InvoiceDataPath + "/" + invoiceID + ".json");
            InvoiceDetailsDataModel jsonObject = JsonSerializer.Deserialize<InvoiceDetailsDataModel>(jsonData);

            jsonObject.paid_amount = jsonObject.paid_amount+amountPaid;
            
            float amountRemaining = jsonObject.amount - jsonObject.paid_amount;
            if (amountRemaining <= 0)
            {
                jsonObject.status = InvoiceStatus.Paid;
            }
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(jsonObject);
            File.WriteAllText(InvoiceDataPath + "/" + invoiceID + ".json",jsonContent);
            return 1;
        }
        public int GetJsonContent(InvoiceDetailsDataModel invoiceDetailsData)
        {
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(invoiceDetailsData);
            string fileName = invoiceDetailsData.id + ".json";
            File.WriteAllText(InvoiceDataPath + "/" + fileName, jsonContent);
            return 1;
        }
        public IEnumerable<InvoiceDetailsDataModel> GetAllInvoices(int filterStatusUnPaid = 0)
        {
            List<InvoiceDetailsDataModel> invoiceDetailsArray = new List<InvoiceDetailsDataModel>();
            foreach (string invoice in Directory.GetFiles(InvoiceDataPath))
            {
                InvoiceDetailsDataModel invoiceDetails = new InvoiceDetailsDataModel();
                invoiceDetails = JsonSerializer.Deserialize<InvoiceDetailsDataModel>(File.ReadAllText(invoice));
                if (filterStatusUnPaid ==1)
                {
                    if (invoiceDetails.status != InvoiceStatus.Paid)
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
        public int GetMasterData()
        {
            string jsonData = File.ReadAllText(MasterDataPath);
            
            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonData);
            return jsonObject["invoiceId"];
        }
    }
}
