using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Dtos.UserDtos;

using Microsoft.AspNetCore.Mvc;

namespace DocGen.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterUser(UserRegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest(registerDto);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(registerDto);
            }

            try
            {
                var result = await userService.RegisterUserAsync(registerDto);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (ArgumentNullException)
            {
                return BadRequest(registerDto);
            }
            catch (EntityAlreadyExistsException)
            {
                return StatusCode(StatusCodes.Status409Conflict, registerDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, registerDto);
            }

        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoginUser(UserLoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest(loginDto);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(loginDto);
            }

            try
            {
                var result = await userService.LoginUserAsync(loginDto);

                if (result.Result)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, loginDto);
                }
            }
            catch (ArgumentNullException)
            {
                return BadRequest(loginDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, loginDto);
            }
        }

        [HttpGet]
        [Route("get: {id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserInfo(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(id);
            }

            try
            {
                var result = await userService.GetClientInfoAsync(id);

                if (result == null)
                {
                    return NotFound(false);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, id);
            }
        }

        [HttpPut]
        [Route("pass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ChangePassword(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (userPasswordChangeDto == null)
            {
                return BadRequest(userPasswordChangeDto);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(userPasswordChangeDto);
            }

            try
            {
                var result = await userService.ChangeUserPasswordAsync(userPasswordChangeDto);

                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, false);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, userPasswordChangeDto);
            }
        }
    }
}
