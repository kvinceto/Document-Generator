using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Dtos.CompanyDtos;

using Microsoft.AspNetCore.Mvc;

namespace DocGen.Api.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        [Route("all_companies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllCompanies()
        {
            var result = await companyService.GetAllActiveAndDeletedCompaniesAsync();

            return Ok(result);
        }


        [HttpGet]
        [Route("all_active_companies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllActiveCompanies()
        {
            var result = await companyService.GetAllActiveCompaniesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("all_deleted_companies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllDeletedCompanies()
        {
            var result = await companyService.GetAllDeletedCompaniesAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("create_company")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCompany(CompanyDtoAdd companyDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, companyDto);
            }

            try
            {
                bool result = await companyService.CreateCompanyAsync(companyDto);

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
                return StatusCode(StatusCodes.Status409Conflict, companyDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpPut]
        [Route("edit_company")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditCompany(CompanyDtoEdit companyDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, companyDto);
            }

            try
            {
                bool result = await companyService.EditCompanyDataAsync(companyDto);

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
                return StatusCode(StatusCodes.Status404NotFound, companyDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpGet]
        [Route("get_company_info: {companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCompanyInfo(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, companyId);
            }

            try
            {
                var result = await companyService.GetCompanyInfoAsync(companyId);

                return Ok(result);
            }
            catch (EntityDoNotExistsException)
            {
                return StatusCode(StatusCodes.Status404NotFound, companyId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpDelete]
        [Route("delete_company")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCompany(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, companyId);
            }

            try
            {
                bool result = await companyService.DeleteCompanyAsync(companyId);

                return Ok(result);
            }
            catch (EntityDoNotExistsException)
            {
                return StatusCode(StatusCodes.Status404NotFound, companyId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }
    }
}
