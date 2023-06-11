using Domain.Models;

namespace Repository.Business_Models
{
    public class UserBusiness:IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
