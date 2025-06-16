
namespace Mission.Entities.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        
        public string UserImage { get; set; }
    }
}
