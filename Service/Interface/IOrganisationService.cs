using User_Registartion.Entity;
using User_Registartion.Model;

namespace User_Registartion.Service.Interface
{
    public interface IOrganisationService
    {
        public Task<Organisation> GetOrganisationById(string Id);
        public Task<Organisation> CreateOrganisation(OrganisationRequestModel requestModel);
        public Task<Organisation> AddUserToAnOrganisation(User user, string organisationId);
    }
}
