using AdvertApp.ApplicationServices;
using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.EF.Entities;
using AdvertApp.Models.Enums;
using AdvertApp.Models.FormModels;
using AdvertApp.Repositories.Contracts;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertAppTests
{
    public class AdvertApplicationServiceTests
    {
        private readonly Mock<IAdvertReadRepository> _advertReadRepository;
        private readonly Mock<IAdvertWriteRepository> _advertWriteRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<IAdvertApplicationService>> _logger;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IImageApplicationService> _imageApplicationService;
        private readonly Mock<IMapper> _mapper;

        private readonly IAdvertApplicationService _sut;

        public UpdateAdvertFormModel updateformModel;
        public AdvertApplicationServiceTests()
        {
            _advertReadRepository = new Mock<IAdvertReadRepository>();
            _advertWriteRepository = new Mock<IAdvertWriteRepository>();
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<IAdvertApplicationService>>();
            _configuration = new Mock<IConfiguration>();
            var inMemorySettings = new Dictionary<string, string>();
            inMemorySettings.Add("SettingsPerUserOptions:MaxAdvertAmount", "20");

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _imageApplicationService = new Mock<IImageApplicationService>();
            _mapper = new Mock<IMapper>();


            _sut = new AdvertApplicationService(
                _advertReadRepository.Object,
                _advertWriteRepository.Object,
                _userRepository.Object,
                _logger.Object,
                configuration,
                _imageApplicationService.Object,
                _mapper.Object);

            updateformModel = new UpdateAdvertFormModel
            {
                AdvertId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Text = "Test update advert text",
                Image = null,
                Status = AdvertStatus.Active
            };
        }

        [Fact]
        public async Task AddAsync_ValidValue_ResultSuccess()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Role = UserRole.Consumer
            };

            var formModel = new CreateAdvertFormModel
            {
                UserId = user.Id,
                Text = "Test advert text",
                Image = null
            };

            _advertReadRepository
                .Setup(a =>
                a.GetByUserIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<IEnumerable<Advert>>(new List<Advert> { }));
            _userRepository
                .Setup(u =>
                u.GetByIdAsync(user.Id)).Returns(Task.FromResult<User?>(user));


            var result = await _sut.AddAsync(formModel);

            Assert.True(result.IsSuccess);
            _advertWriteRepository
                .Verify(a => a.CreateAsync(It.IsAny<Advert>()),
                Times.Once);
        }

        [Fact]
        public async Task AddAsync_UserDoesNotExist_ResultFailure()
        {
            var formModel = new CreateAdvertFormModel
            {
                UserId = Guid.NewGuid(),
                Text = "Test advert text",
                Image = null
            };

            _advertReadRepository
                .Setup(a =>
                a.GetByUserIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<IEnumerable<Advert>>(new List<Advert> { }));

            _userRepository
                .Setup(u =>
                u.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<User?>(null));

            var result = await _sut.AddAsync(formModel);

            Assert.True(result.IsFailure);
            _advertWriteRepository
                .Verify(a => a.CreateAsync(It.IsAny<Advert>()),
                Times.Never);
        }

        [Fact]
        public async Task Update_ValidValue_ResultSuccess()
        {
            var advert = _advertReadRepository
                .Setup(a => a.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Advert?>(new Advert(
                    updateformModel.UserId,
                    "UserName",
                    updateformModel.Text,
                    null)));

            var result = await _sut.Update(updateformModel);

            Assert.True(result.IsSuccess);
            _advertWriteRepository
                .Verify(a => a.Update(It.IsAny<Advert>()),
                Times.Once);
        }

        [Fact]
        public async Task Update_AdminUserWithValidValue_ResultSuccess()
        {
            var advert = _advertReadRepository
                .Setup(a => a.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Advert?>(new Advert(
                    Guid.NewGuid(),
                    "UserName",
                    updateformModel.Text,
                    null)));

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Role = UserRole.Admin
            };

            _userRepository
                .Setup(u =>
                u.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult<User?>(user));

            var result = await _sut.Update(updateformModel);

            Assert.True(result.IsSuccess);
            _advertWriteRepository
                .Verify(a => a.Update(It.IsAny<Advert>()),
                Times.Once);
        }

        [Fact]
        public async Task Update_AdvertDoesNotExist_ResultFalure()
        {
            var advert = _advertReadRepository
                .Setup(a => a.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Advert?>(null));

            var result = await _sut.Update(updateformModel);

            Assert.True(result.IsFailure);
            _advertWriteRepository
                .Verify(a => a.Update(It.IsAny<Advert>()),
                Times.Never);
        }

        [Fact]
        public async Task Update_UserHasNoPermissions_ResultFalure()
        {
            var advert = _advertReadRepository
                .Setup(a => a.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Advert?>(new Advert(
                    Guid.NewGuid(),
                    "UserName",
                    updateformModel.Text,
                    null)));

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Role = UserRole.Consumer
            };

            _userRepository
                .Setup(u =>
                u.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult<User?>(user));

            var result = await _sut.Update(updateformModel);

            Assert.True(result.IsFailure);
            _advertWriteRepository
                .Verify(a => a.Update(It.IsAny<Advert>()),
                Times.Never);
        }
    }
}