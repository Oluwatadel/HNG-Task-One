using User_Registartion.Entity;
using User_Registartion.Model;

namespace User_Registartion.Service.Interface
{
    public interface IUserService
    {
        public Task<User> Register(UserRegRequestModel requestModel);
        public Task<User> GetUser(string email);
    }
}
