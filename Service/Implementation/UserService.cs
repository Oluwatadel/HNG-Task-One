using User_Registartion.Entity;
using User_Registartion.Model;
using User_Registartion.Repository.Interface;
using User_Registartion.Service.Interface;

namespace User_Registartion.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IOrganisationRepository organisationRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _organisationRepository = organisationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUser(string emailOrUserId)
        {
            var user = await _userRepository.GetUser(emailOrUserId);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User> Register(UserRegRequestModel requestModel)
        {
            var user = await _userRepository.GetUser(requestModel.Email!);
            if (user != null)
            {
                return null;
            }

            var newOrganisation = new Organisation
            {
                Name = $"{requestModel.LastName!.Substring(2)}{requestModel.FirstName!.Substring(2)}{new Random().Next(91, 106)}",
                Description = "THe organisation was formed using the name and random generation of the number",
            };

            var newUser = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password),
                Phone = requestModel.Phone,
            };

            newOrganisation.Users = new List<User>();
            newUser.Organisations = new List<Organisation>();

            newOrganisation.Users!.Add(newUser);
            newUser.Organisations!.Add(newOrganisation);

            await _userRepository.AddUser(newUser);
            await _organisationRepository.AddOrganisation(newOrganisation);
            var returnValue = await _unitOfWork.SaveChanges();

            if(returnValue <= 0)
            {
                return null;
            }
            return newUser;
            
        }
    }
}
