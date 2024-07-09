using Microsoft.EntityFrameworkCore;
using User_Registartion.Context;
using User_Registartion.Entity;
using User_Registartion.Repository.Interface;

namespace User_Registartion.Repository.Implementation
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly UserDBContext _userDBContext;

        public OrganisationRepository(UserDBContext userDBContext)
        {
            _userDBContext = userDBContext;
        }

        public async Task<Organisation> AddOrganisation(Organisation organisation)
        {
            await _userDBContext.Organisations.AddAsync(organisation);
            return organisation;
        }

        public async Task<Organisation> GetOrganisation(string userId)
        {
            var organisation = await _userDBContext.Organisations.FirstOrDefaultAsync(a => a.OrgId == userId);
            return organisation!;
        }
    }
}
