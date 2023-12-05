using HumanRegisterWebApi.Controllers;
using HumanRegisterWebApi.Services.Adapters;
using HumanRegisterWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static HumanRegisterWebApi.Enums.Enums;

namespace UserControllerTests
{
    public class UnitTest1
    {



        [Fact]
        public async Task GetUserInformationDto_ReturnsOkObjectResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var userName = "testUser";
            var expectedDto = new GetUpsertDTO();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns(new Claim(ClaimTypes.Name, userName));

            var controller = new UserController(serviceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext.Object }
            };

            serviceMock.Setup(x => x.GetUserInformationDto(userName)).ReturnsAsync(new OkObjectResult(expectedDto));

            // Act
            var result = await controller.GetUserInformationDto();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedDto, okObjectResult.Value);
        }

        [Fact]
        public async Task GetUserInformationDto_ReturnsUnauthorizedResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns((Claim)null);

            var controller = new UserController(serviceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext.Object }
            };

            serviceMock.Setup(x => x.GetUserInformationDto(It.IsAny<string>())).ReturnsAsync(new UnauthorizedResult());

            // Act
            var result = await controller.GetUserInformationDto();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }



        [Fact]
        public async Task RegisterUserInfoAndUserAddress_ValidRequest_ReturnsOkObjectResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);

            var requestUserData = new GetUpsertNoImageDTO(); 
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns(new Claim(ClaimTypes.Name, "testUser"));
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            serviceMock.Setup(x => x.RegisterUserInfoAndUserAddress("testUser", requestUserData))
                .ReturnsAsync(new OkObjectResult("New user data was added"));

            // Act
            var result = await controller.RegisterUserInfoAndUserAddress(requestUserData);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("New user data was added", okObjectResult.Value);
        }
        [Fact]
        public async Task RegisterUserInfoAndUserAddress_UserAlreadyRegistered_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);

            var requestUserData = new GetUpsertNoImageDTO(); 

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns(new Claim(ClaimTypes.Name, "testUser"));
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            serviceMock.Setup(x => x.RegisterUserInfoAndUserAddress("testUser", requestUserData))
                .ReturnsAsync(new BadRequestObjectResult("Already fully registered, you only can change fields one by one"));

            // Act
            var result = await controller.RegisterUserInfoAndUserAddress(requestUserData);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Already fully registered, you only can change fields one by one", badRequestObjectResult.Value);
        }

        [Fact]
        public async Task RegisterUserInfoAndUserAddress_UnsuccessfulUpdate_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);

            var requestUserData = new GetUpsertNoImageDTO();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns(new Claim(ClaimTypes.Name, "testUser"));
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            serviceMock.Setup(x => x.RegisterUserInfoAndUserAddress("testUser", requestUserData))
                .ReturnsAsync(new BadRequestObjectResult("Database not updated"));

            // Act
            var result = await controller.RegisterUserInfoAndUserAddress(requestUserData);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Database not updated", badRequestObjectResult.Value);
        }


        [Fact]
        public async Task UpdateFirstName_WithValidRequest_ReturnsOkObjectResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);

            var request = "NewFirstName"; 

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns(new Claim(ClaimTypes.Name, "testUser"));
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            serviceMock.Setup(x => x.UpdateField<string>("FirstName", "testUser", request))
                .ReturnsAsync(new OkObjectResult("Values was changed"));

            // Act
            var result = await controller.UpdateFirstName(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Values was changed", okObjectResult.Value);
        }

        [Fact]
        public async Task UpdateFirstName_WhenChangeFieldFails_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var controller = new UserController(serviceMock.Object);

            var request = "NewFirstName";

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(c => c.User.FindFirst(ClaimTypes.Name)).Returns(new Claim(ClaimTypes.Name, "testUser"));
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            serviceMock.Setup(x => x.UpdateField<string>("FirstName", "testUser", request))
                .ReturnsAsync(new BadRequestObjectResult("Not changed"));

            // Act
            var result = await controller.UpdateFirstName(request);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Not changed", badRequestObjectResult.Value);
        }


    }
}