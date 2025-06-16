using BooksApi.Data.Models;
using BooksApi.Entities.Entities;

namespace BooksApi.Entities.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllUser(FilterVM filterRequest);
    }
}
