using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using DocGen.Core.Contracts;
using DocGen.Dtos.UserDtos;

using static DocGen.Common.ApplicationGlobalConstants;
using DocGen.Common.CustomClases;

namespace DocGen.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser<string>> userManager;
        private readonly SignInManager<IdentityUser<string>> signInManager;
        private readonly IConfiguration configuration;
        private readonly IModelFactory modelFactory;

        public UserService(UserManager<IdentityUser<string>> userManager, SignInManager<IdentityUser<string>> signInManager, IConfiguration configuration, IModelFactory modelFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.modelFactory = modelFactory;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var existingClient = await userManager.FindByEmailAsync(userDto.Email);

            if (existingClient != null)
            {
                throw new EntityAlreadyExistsException();
            }

            IdentityUser user = this.modelFactory.CreateUser(userDto);

            var result = await this.userManager.CreateAsync(user, userDto.Password);

            await userManager.AddToRoleAsync(user, UserRoleName);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserReturnDto> LoginUserAsync(UserLoginDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var user = await this.userManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                return new UserReturnDto() { Result = false };
            }

            var passwordMatches = await this.userManager.CheckPasswordAsync(user, userDto.Password);

            if (!passwordMatches)
            {
                return new UserReturnDto() { Result = false };
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            var roles = await userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                var dto = new UserReturnDto()
                {
                    Id = user.Id,
                    Email = userDto.Email,
                    Result = true,
                };
                dto.Token = await this.GenerateJwtTokenAsync((IdentityUser)user);
                dto.RoleName = string.Join(",", roles);

                return dto;
            }
            else
            {
                return new UserReturnDto { Result = false };
            }
        }

        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            // Check if required configuration values are present
            if (string.IsNullOrEmpty(configuration["JwtSecret"]) ||
                string.IsNullOrEmpty(configuration["JwtIssuer"]) ||
                string.IsNullOrEmpty(configuration["JwtAudience"]))
            {
                throw new ApplicationException("JWT configuration values are missing or invalid.");
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = DateTime.UtcNow.AddDays(7);

            // Ensure the key used for signing has at least 256 bits (32 bytes)
            var jwtSecret = configuration["JwtSecret"];
            if (jwtSecret.Length < 32)
            {
                throw new ArgumentException("JWT secret must be at least 32 characters long.");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtIssuer"],
                audience: configuration["JwtAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserFullInfoDto?> GetClientInfoAsync(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                return null;
            }

            return new UserFullInfoDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<bool> ChangeUserPasswordAsync(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (userPasswordChangeDto == null)
            {
                throw new ArgumentNullException(nameof(userPasswordChangeDto));
            }

            var user = await userManager.FindByEmailAsync(userPasswordChangeDto.Email);

            if (user == null)
            {
                // Client not found
                return false;
            }

            var result = await userManager.ChangePasswordAsync(user, userPasswordChangeDto.OldPassword, userPasswordChangeDto.NewPassword);

            if (result.Succeeded)
            {
                // Password changed successfully
                return true;
            }
            else
            {
                // Failed to change password
                return false;
            }
        }
    }
}
