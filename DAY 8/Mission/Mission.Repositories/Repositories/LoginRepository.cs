using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories
{
    public class LoginRepository(MissionDbContext cIDbContext) : ILoginRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public LoginUserResponseModel LoginUser(LoginUserRequestModel model)
        {
            var existingUser = _cIDbContext.User
                .FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower() && !u.IsDeleted);

            if (existingUser == null)
            {
                return new LoginUserResponseModel() { Message = "Email Address Not Found." };
            }
            if (existingUser.Password != model.Password)
            {
                return new LoginUserResponseModel() { Message = "Incorrect Password." };
            }

            return new LoginUserResponseModel
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName ?? string.Empty,
                LastName = existingUser.LastName ?? string.Empty,
                PhoneNumber = existingUser.PhoneNumber,
                EmailAddress = existingUser.EmailAddress,
                UserType = existingUser.UserType,
                UserImage = existingUser.UserImage != null ? existingUser.UserImage : string.Empty,
                Message = "Login Successfully"
            };
        }

        public async Task<string> Register(RegisterUserModel model)
        {
            var isExist = _cIDbContext.User.Where(x => x.EmailAddress == model.EmailAddress && !x.IsDeleted).FirstOrDefault();

            if (isExist != null) throw new Exception("Email already exist");

            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                UserType = "user",
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
            };

            await _cIDbContext.User.AddAsync(user);
            _cIDbContext.SaveChanges();
            return "User Added!";
        }
    }
}
