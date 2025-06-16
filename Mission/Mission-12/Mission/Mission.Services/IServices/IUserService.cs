
using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseModel> GetUserById(int id);
        string UserDelete(int id);
        Task<List<UserResponseModel>> GetAllUsers();
    }

}
