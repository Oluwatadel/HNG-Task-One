using User_Registartion.Context;
using User_Registartion.Repository.Interface;

namespace User_Registartion.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDBContext _dbContext;

        public UnitOfWork(UserDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChanges()
        {
           var returnValue = await _dbContext.SaveChangesAsync();
           return returnValue;
        }
    }
}
