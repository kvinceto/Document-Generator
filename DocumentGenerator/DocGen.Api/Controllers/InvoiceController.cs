using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Dtos.InvoiceDtos;

using Microsoft.AspNetCore.Mvc;

namespace DocGen.Api.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("all_by_company: {companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllInvoicesByCompany(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                return BadRequest(companyId);
            }

            try
            {
                var result = await invoiceService.GetAllInvoicesByCompany(companyId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpGet]
        [Route("all_by_client: {clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllInvoicesByClient(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                return BadRequest(clientId);
            }

            try
            {
                var result = await invoiceService.GetAllInvoicesByClient(clientId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpGet]
        [Route("all_by_user: {userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllInvoicesByUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(userId);
            }

            try
            {
                var result = await invoiceService.GetAllInvoicesByUser(userId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpGet]
        [Route("get: {invoiceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetInvoiceById(int? invoiceId)
        {
            if (invoiceId == null)
            {
                return BadRequest(invoiceId);
            }

            try
            {
                var invoice = await invoiceService.GetInvoiceFullInfo(invoiceId.Value);
                return Ok(invoice);
            }
            catch (EntityDoNotExistsException)
            {
                return NotFound(invoiceId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateInvoice(InvoiceDtoAdd invoiceDto)
        {
            if (invoiceDto == null)
            {
                return BadRequest(invoiceDto);
            }

            try
            {
                bool result = await invoiceService.CreateInvoiceAsync(invoiceDto);

                if (result)
                {
                    return Ok(invoiceDto);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (EntityAlreadyExistsException)
            {
                return StatusCode(StatusCodes.Status409Conflict, invoiceDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpPut]
        [Route("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditInvoice(InvoiceDtoEdit invoiceDto)
        {
            if (invoiceDto == null)
            {
                return BadRequest(invoiceDto);
            }

            try
            {
                bool result = await invoiceService.EditInvoiceAsync(invoiceDto);

                if (result)
                {
                    return Ok(invoiceDto);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (EntityDoNotExistsException)
            {
                return NotFound(invoiceDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpDelete]
        [Route("delete: {invoiceId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteInvoice(int? invoiceId)
        {
            if (invoiceId == null)
            {
                return BadRequest(invoiceId);
            }

            try
            {
                bool result = await invoiceService.DeleteInvoiceAsync(invoiceId.Value);

                if (result)
                {
                    return StatusCode(StatusCodes.Status202Accepted, true);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (EntityDoNotExistsException)
            {
                return NotFound(invoiceId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }
    }
}
