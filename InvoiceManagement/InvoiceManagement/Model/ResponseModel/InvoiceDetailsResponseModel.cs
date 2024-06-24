namespace InvoiceManagement.Model.ResponseModel
{
    public class InvoiceDetailsResponseModel
    {
        public int id { get; set; }
        public float amount { get; set; }
        public float paid_amount { get; set; }
        public string due_date { get; set; }
        public string status { get; set; }
    }
}
