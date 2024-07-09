using User_Registartion.Entity;

namespace User_Registartion.Repository.Interface
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChanges();

    }
}
