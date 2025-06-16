using BooksApi.Data.Models;
using BooksApi.Entities.Repositories.Interface;
using BooksApi.Models;
using BooksApi.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserVM>> GetAllUsers(FilterVM filterRequest)
        {
            var users = await _userRepository.GetAllUser(filterRequest);

            return users.Select(u => new UserVM()
            {
                Email = u.Email,
                Id = u.Id,
                Name = u.Name,
                Role = u.Role,
                Book = u.BookDetails.Select(u => new Book()
                {
                    Id = u.Id,
                    Author = u.Author,
                    Description = u.Description,
                    Title = u.Title
                }).ToList()
            }).ToList();
        }

    }
}
