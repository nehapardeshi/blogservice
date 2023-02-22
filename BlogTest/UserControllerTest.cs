using Blog.WebAPI.Controllers;
using Blog.WebAPI.Models;
using BlogTest.Mock;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Assert = Xunit.Assert;
using User = Blog.WebAPI.Models.User;

namespace BlogTest
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly IUserService _userService;

        public UserControllerTest()
        {
            _userService = new UserServiceMock();
            _controller = new UserController(_userService);
        }

        [Fact]
        public async Task LoginUser()
        {
            var loginUserResult = await _controller.LoginUser(new LoginInfo { Email = "userone@test.com", Password = "user1" });
            var okResult = loginUserResult as OkObjectResult;

            Assert.NotNull(okResult);
            var user = Assert.IsType<User>(okResult.Value);

            Assert.NotNull(user);

            Assert.Equal(user.Id, 1);
            Assert.Equal(user.Email, "userone@test.com");
        }

        [Fact]
        public async Task GetUser()
        {
            var getUserResult = await _controller.GetUser(1);
            var okResult = getUserResult as OkObjectResult;

            Assert.NotNull(okResult);
            var user = Assert.IsType<User>(okResult.Value);

            Assert.NotNull(user);

            Assert.Equal(user.Id, 1);
            Assert.Equal(user.Email, "userone@test.com");
        }

        [Fact]
        public async Task AddUser()
        {
            var addUserResult = await _controller.AddUser(new AddUserRequest { Email = "addedbytest@test.com", FirstName = "UserTest", LastName = "JustTest", Password = "testpassword" });
            var okResult = addUserResult as OkObjectResult;

            Assert.NotNull(okResult);
            var userId = Assert.IsType<int>(okResult.Value);

            Assert.Equal(userId, 3);
        }

        [Fact]
        public async Task UpdatePassword()
        {
            var addUserResult = await _controller.UpdatePassword(new PasswordResetRequest { Email = "userone@test.com", NewPassword = "testpassword" });
            var okResult = addUserResult as OkObjectResult;

            Assert.NotNull(okResult);
            var success = Assert.IsType<bool>(okResult.Value);

            Assert.Equal(success, true);
        }
    }
}
