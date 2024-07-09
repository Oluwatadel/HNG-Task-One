using User_Registartion.Entity;

namespace User_Registartion.Service.Interface
{
    public interface IIdentityService
    {
        public Task<bool> IsValidUser(string email);
    }
}
