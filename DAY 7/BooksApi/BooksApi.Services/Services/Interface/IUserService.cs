using BooksApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Services.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserVM>> GetAllUsers(FilterVM filterRequest);
    }
}
