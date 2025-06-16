using Mission.Entities;
using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.IServices
{
    public interface ILoginService
    {
        ResponseResult LoginUser(LoginUserRequestModel model);

        LoginUserResponseModel UserLogin(LoginUserRequestModel model);
        Task<string> Register(RegisterUserModel model);
    }
}
