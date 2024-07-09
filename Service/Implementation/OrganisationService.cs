using User_Registartion.Entity;
using User_Registartion.Model;
using User_Registartion.Repository.Interface;
using User_Registartion.Service.Interface;

namespace User_Registartion.Service.Implementation
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrganisationService(IOrganisationRepository organisationRepository, IUnitOfWork unitOfWork)
        {
            _organisationRepository = organisationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Organisation> AddUserToAnOrganisation(User user, string organisationId)
        {
            var organisation = await _organisationRepository.GetOrganisation(organisationId);
            organisation.Users.Add(user);
            await _unitOfWork.SaveChanges();
            return organisation;
        }

        public async Task<Organisation> CreateOrganisation(OrganisationRequestModel requestModel)
        {
            var organisation = new Organisation
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
            };
            await _organisationRepository.AddOrganisation(organisation);
            await _unitOfWork.SaveChanges();
            return organisation;
        }

        public async Task<Organisation> GetOrganisationById(string Id)
        {
            var organisation = await _organisationRepository.GetOrganisation(Id);
            return organisation;
        }
    }
}
