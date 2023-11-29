using AdvertApp.EF.Entities;

namespace AdvertApp.ApplicationServices.Contracts
{
    public interface IUserApplicationService
    {
        /// <summary>
        /// Get User By Id.
        /// </summary>
        /// <param name="id">Id of searched user.</param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(Guid id);
    }
}
