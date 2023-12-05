using HumanRegisterWebApi.Controllers;
using HumanRegisterWebApi.Services.Data;
using HumanRegisterWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using static HumanRegisterWebApi.Enums.Enums;

namespace AdminControllerTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserIdDoesNotExist()
        {
            // Arrange
            var adminServiceMock = new Mock<IAdminService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<AdminController>>();

            var controller = new AdminController(adminServiceMock.Object, userRepositoryMock.Object, loggerMock.Object);

            var nonExistentUserId = Guid.NewGuid();
            adminServiceMock.Setup(x => x.CheckUserIdExists(nonExistentUserId)).ReturnsAsync(false);

            // Act
            var result = await controller.GetCurentUserInfo(nonExistentUserId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"{nonExistentUserId} not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsBadRequest_WhenDeletingCurrentUserAccount()
        {
            // Arrange
            var adminServiceMock = new Mock<IAdminService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<AdminController>>();

            var controller = new AdminController(adminServiceMock.Object, userRepositoryMock.Object, loggerMock.Object);

            var currentUserId = Guid.NewGuid();
            var currentUserName = "currentuser";
            var userClaims = new[] { new Claim(ClaimTypes.Name, currentUserName), new Claim(ClaimTypes.Role, Role.Admin.ToString()) };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.User).Returns(userPrincipal);
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            adminServiceMock.Setup(x => x.CheckUserIdExists(currentUserId)).ReturnsAsync(true);
            adminServiceMock.Setup(x => x.CheckCurrentUserWithGivenUserId(currentUserName, currentUserId)).ReturnsAsync(true);

            // Act
            var result = await controller.GetCurentUserInfo(currentUserId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("You cannot delete your account", badRequestResult.Value);
        }

    }
}