using InvoiceManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.InvoiceService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceManagement.Controllers
{
    [Route("invoices")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        public IInvoiceService _invoiceService { get; set; }
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
                
        }
        // GET: api/<InvoiceController>
        [HttpGet]
        public IEnumerable<InvoiceDetailsViewModel> Get()
        {
            return _invoiceService.GetAllInvoiceDetails();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST Invoice
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateInvoiceViewModel _invoiceModel)
        {
            InvoiceDetailsViewModel response = _invoiceService.SaveInvoiceDetails(_invoiceModel);
            
            return Ok(response.id);

        }

        // PUT 
        [HttpPut("{id}/payments")]
        public IActionResult Put(int id, [FromBody] PaymentViewModel paymentAmount)
        {
            var respone = _invoiceService.MakePayments(id, paymentAmount.amount);
            return Ok(respone);
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
