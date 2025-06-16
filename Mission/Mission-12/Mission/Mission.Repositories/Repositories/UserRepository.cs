using Microsoft.EntityFrameworkCore;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.Repositories
{
    public class UserRepository(MissionDbContext cIDbContext) : IUserRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public string DeleteUser(int id)
        {
            var user = _cIDbContext.User.Where(x => x.Id == id).FirstOrDefault();

            if (user == null) throw new Exception("Account does't exist!");

            user.IsDeleted = true;

            //user.EmailAddress = model.EmailAddress

            user.ModifiedDate = DateTime.UtcNow;
            _cIDbContext.User.Update(user);
            _cIDbContext.SaveChanges();
            return "Account deleted!";
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _cIDbContext.User
                .Where(u => u.Id == id && !u.IsDeleted)
                .Select(u => new UserResponseModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    EmailAddress = u.EmailAddress,
                    PhoneNumber = u.PhoneNumber,
                    UserType = u.UserType,

                    // Pull UserImage from related UserDetail table if available
                    UserImage = _cIDbContext.UserDetails
                        .Where(ud => ud.UserId == u.Id && !ud.IsDeleted)
                        .Select(ud => ud.UserImage)
                        .FirstOrDefault() ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not exist");

            return user;
        }

        public async Task<List<UserResponseModel>> GetAllUsers()
        {
            return await _cIDbContext.User.Where(u => !u.IsDeleted)
                .Select(user => new UserResponseModel()
                {
                    EmailAddress = user.EmailAddress,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserType = user.UserType
                }).ToListAsync();
        }
    }
}
