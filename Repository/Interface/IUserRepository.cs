using User_Registartion.Context;
using User_Registartion.Entity;

namespace User_Registartion.Repository.Interface
{
    public interface IUserRepository
    {


        public Task<User> AddUser(User user);
        public Task<User> GetUser(string emailOrUseId);

    }
}
