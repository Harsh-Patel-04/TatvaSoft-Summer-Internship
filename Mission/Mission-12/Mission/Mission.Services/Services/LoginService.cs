using Mission.Services.IServices;
using System;
using Mission.Repositories.IRepositories;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Repositories.Helpers;

namespace Mission.Services
{
    public class LoginService(ILoginRepository loginRepository, JwtService jwtService) : ILoginService
    {
        private readonly ILoginRepository _loginRepository = loginRepository;
        private readonly JwtService _jwtService = jwtService;
        ResponseResult result = new ResponseResult();

        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            var userObj = UserLogin(model);

            if (userObj.Message.ToString() == "Login Successfully")
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Success;
                result.Data = _jwtService.GenerateToken(userObj.Id.ToString(), userObj.FirstName, userObj.LastName, userObj.PhoneNumber, userObj.EmailAddress, userObj.UserType, userObj.UserImage);
            }
            else
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }

        public LoginUserResponseModel UserLogin(LoginUserRequestModel model)
        {
            return _loginRepository.LoginUser(model);
        }

        public async Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest)
        {
            return await _loginRepository.RegisterUser(registerUserRequest);
        }

        public async Task<bool> UpdateUser(UserUpdateModel updatedUser)
        {
            var user = await _loginRepository.GetByIdAsync(updatedUser.Id);
            if (user == null) return false;

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.EmailAddress = updatedUser.EmailAddress;
            user.UserImage = updatedUser.UserImage;
            user.ModifiedDate = DateTime.UtcNow;

            await _loginRepository.SaveAsync(); // ✅ This line is required

            return true;
        }

        public UserResponseModel LoginUserDetailById(int id)
        {
            return _loginRepository.LoginUserDetailById(id);
        }

        public UserProfileResponseModel GetUserProfileDetailById(int id)
        {
            return _loginRepository.GetUserProfileDetailById(id);
        }


        public async Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel)
        {
            return await _loginRepository.LoginUserProfileUpdate(requestModel);
        }

    }
}
