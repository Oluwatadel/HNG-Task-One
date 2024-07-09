using User_Registartion.Entity;

namespace User_Registartion.Repository.Interface
{
    public interface IOrganisationRepository
    {
        public Task<Organisation> AddOrganisation(Organisation organisation);
        public Task<Organisation> GetOrganisation(string userId);
        //public Task UnitOfWork(Organisation organisation);

    }
}
