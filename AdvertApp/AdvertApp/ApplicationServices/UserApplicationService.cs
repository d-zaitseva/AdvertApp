using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.EF.Entities;
using AdvertApp.Models.ViewModels;
using AdvertApp.Models;
using AdvertApp.Repositories.Contracts;
using AutoMapper;

namespace AdvertApp.ApplicationServices
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IUserApplicationService> _logger;

        public UserApplicationService(
            IUserRepository userRepository,
            ILogger<IUserApplicationService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }
}
