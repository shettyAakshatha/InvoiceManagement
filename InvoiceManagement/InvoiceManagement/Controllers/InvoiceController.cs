using InvoiceManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.InvoiceService;
using AutoMapper;
using InvoiceManagement.Model.ResponseModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceManagement.Controllers
{
    [Route("invoices")]
    [ApiController]
    [ValidateAtrribute]
    public class InvoiceController : ControllerBase
    {

        public IInvoiceService _invoiceService { get; set; }
        private IMapper _mapper { get; set; }
        private readonly ILogger<InvoiceController> _logger;
       
        public InvoiceController(IInvoiceService invoiceService, IMapper mapper, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
            _logger = logger;
            
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("Get all InvoiceDetails");
                IEnumerable<InvoiceDetailsViewModel> viewModels = _invoiceService.GetAllInvoiceDetails();
                var response = _mapper.Map<IEnumerable<InvoiceDetailsResponseModel>>(viewModels);

                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ");
                return StatusCode(500, new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Server error",
                    Detail = "Server error Try again."
                });

            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                _logger.LogInformation("Get all InvoiceDetails by id");
                if (id == 0)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Invalid input",
                        Detail = "Id must be greater than or equal to 0"
                    };
                    return BadRequest(problemDetails);
                }
                var response = _invoiceService.GetInvoiceDetails(id);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;

            }

        }

        
        [HttpPost]
        public IActionResult Post([FromBody] CreateInvoiceViewModel _invoiceModel)
        {
            _logger.LogInformation("Create Invoice");
            try
            {
                InvoiceDetailsViewModel response = _invoiceService.SaveInvoiceDetails(_invoiceModel);
                if (response != null)
                {

                    return CreatedAtAction("Get", new { id = response.id });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw;
                

            }

        }

        // PUT 
        [HttpPut("{id}/payments")]
        public IActionResult Put(int id, [FromBody] PaymentViewModel paymentAmount)
        {
            _logger.LogInformation("Make Payments");
            try
            {
                if (id <= 0)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Invalid input",
                        Detail = "Id must be greater than or equal to 0"
                    };
                    return BadRequest(problemDetails);
                }
                var respone = _invoiceService.MakePayments(id, paymentAmount.amount);
                if (respone == 1)
                {
                    return NoContent();

                }
                else
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Invalid input",
                        Detail = "Payment Faied Try again"
                    };
                    return BadRequest(problemDetails);
                }
            }
            catch (Exception ex)
            {
                throw;


            }

        }

        
        [HttpPost("/process-overdue")]
        public IActionResult ProcessOverDue([FromBody] OverdueViewModel overdue)
        {
            _logger.LogInformation("Process OverDue Invoices");
            try
            {

                int re = _invoiceService.ProcessOverDue(overdue.late_fee, overdue.overdue_days);
                if (re == 0)
                {
                    return StatusCode(400, new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Title = "Exception in Processing ",
                        Detail = "Can't process the overdue details."
                    });
                }
                return Ok();
            }
            catch (Exception ex) {
                throw;

            }

        }
    }
}
