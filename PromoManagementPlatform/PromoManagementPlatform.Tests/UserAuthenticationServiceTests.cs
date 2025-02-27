using AutoMapper;
using Moq;
using PromoManagementPlatform.Application.DTOs.Login;
using PromoManagementPlatform.Application.DTOs.Register;
using PromoManagementPlatform.Application.Implementations;
using PromoManagementPlatform.Domain.Constants;
using PromoManagementPlatform.Domain.Entities;
using Xunit;
using Microsoft.AspNetCore.Identity;
using PromoManagementPlatform.Application.Abstract;

namespace PromoManagementPlatform.Tests
{
    public class UserAuthenticationServiceTests
    {
        private readonly Mock<IUserManagerWrapper<ApplicationUser>> _userManagerMock;
        private readonly Mock<ITokenGeneratorService> _tokenGeneratorServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserAuthenticationService _userAuthenticationService;

        public UserAuthenticationServiceTests()
        {
            _userManagerMock = new Mock<IUserManagerWrapper<ApplicationUser>>();
            _tokenGeneratorServiceMock = new Mock<ITokenGeneratorService>();
            _mapperMock = new Mock<IMapper>();
            _userAuthenticationService = new UserAuthenticationService(_userManagerMock.Object, _tokenGeneratorServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnInvalidCredentials_WhenUserNotFound()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO("test@example.com", "password");
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginUserDTO.Email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Invalid credentials", result.Errors[0]);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnInvalidCredentials_WhenPasswordIsIncorrect()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO("test@example.com", "password");
            var user = new ApplicationUser();
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginUserDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginUserDTO.Password)).ReturnsAsync(false);

            // Act
            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Invalid credentials", result.Errors[0]);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO("test@example.com", "password");
            var user = new ApplicationUser();
            var roles = new List<string> { "User" };
            var token = "generated_token";

            _userManagerMock.Setup(x => x.FindByEmailAsync(loginUserDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginUserDTO.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);
            _tokenGeneratorServiceMock.Setup(x => x.GenerateTokenAsync(user, roles)).ReturnsAsync(token);

            // Act
            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(token, result.Token);
            Assert.Null(result.Errors);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFailedResult_WhenRegistrationFails()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO("John", "Doe", "test@example.com", "password", "password");
            var user = new ApplicationUser();
            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Registration failed" });

            _mapperMock.Setup(x => x.Map<ApplicationUser>(registerUserDTO)).Returns(user);
            _userManagerMock.Setup(x => x.CreateAsync(user, registerUserDTO.Password)).ReturnsAsync(identityResult);

            // Act
            var result = await _userAuthenticationService.RegisterAsync(registerUserDTO);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Registration failed", result.Errors.First().Description);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnSuccessResult_WhenRegistrationSucceeds()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO("John", "Doe", "test@example.com", "password", "password");
            var user = new ApplicationUser();
            var identityResult = IdentityResult.Success;

            _mapperMock.Setup(x => x.Map<ApplicationUser>(registerUserDTO)).Returns(user);
            _userManagerMock.Setup(x => x.CreateAsync(user, registerUserDTO.Password)).ReturnsAsync(identityResult);
            //_userManagerMock.Setup(x => x.AddToRoleAsync(user, UserRolesConstants.Unverified)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userAuthenticationService.RegisterAsync(registerUserDTO);

            // Assert
            Assert.True(result.Succeeded);
        }
    }
}
