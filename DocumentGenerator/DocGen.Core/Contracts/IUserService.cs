using DocGen.Dtos.UserDtos;

namespace DocGen.Core.Contracts
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegisterDto userDto);

        Task<UserReturnDto> LoginUserAsync(UserLoginDto userDto);

        Task<UserFullInfoDto?> GetClientInfoAsync(string Id);

        Task<bool> ChangeUserPasswordAsync(UserPasswordChangeDto userPasswordChangeDto);
    }
}
