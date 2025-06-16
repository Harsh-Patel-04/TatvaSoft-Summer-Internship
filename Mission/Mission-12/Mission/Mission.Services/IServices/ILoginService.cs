using Mission.Entities;
using Mission.Entities.Models;
namespace Mission.Services.IServices
{
    public interface ILoginService
    {
        ResponseResult LoginUser(LoginUserRequestModel model);

        LoginUserResponseModel UserLogin(LoginUserRequestModel model);
        UserProfileResponseModel GetUserProfileDetailById(int id);
        Task<bool> UpdateUser(UserUpdateModel user);
        Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest);
        UserResponseModel LoginUserDetailById(int id);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);
    }
}
