using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Dtos.ClientDtos;

using Microsoft.AspNetCore.Mvc;

namespace DocGen.Api.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        [Route("all_clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllClients()
        {
            var result = await clientService.GetAllActiveAndDeletedClientsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("all_active_clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllActiveClients()
        {
            var result = await clientService.GetAllActiveClientsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("all_deleted_clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllDeletedClients()
        {
            var result = await clientService.GetAllDeletedClientsAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("create_client")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateClient(ClientDtoAdd clientDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, clientDto);
            }

            try
            {
                bool result = await clientService.CreateClientAsync(clientDto);

                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, true);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, false);
                }
            }
            catch (EntityAlreadyExistsException)
            {
                return StatusCode(StatusCodes.Status409Conflict, clientDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpPut]
        [Route("edit_client")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditClient(ClientDtoAdd clientDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, clientDto);
            }

            try
            {
                bool result = await clientService.EditClientDataAsync(clientDto);

                if (result)
                {
                    return StatusCode(StatusCodes.Status202Accepted, true);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, false);
                }
            }
            catch (EntityDoNotExistsException)
            {
                return StatusCode(StatusCodes.Status404NotFound, clientDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpGet]
        [Route("get_client_info: {clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetClientInfo(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, clientId);
            }

            try
            {
                var result = await clientService.GetClientInfoAsync(clientId);

                return Ok(result);
            }
            catch (EntityDoNotExistsException)
            {
                return StatusCode(StatusCodes.Status404NotFound, clientId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpDelete]
        [Route("delete_client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteClient(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, clientId);
            }

            try
            {
                bool result = await clientService.DeleteClientAsync(clientId);

                return Ok(result);
            }
            catch (EntityDoNotExistsException)
            {
                return StatusCode(StatusCodes.Status404NotFound, clientId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }
    }
}