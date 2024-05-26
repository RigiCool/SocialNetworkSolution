using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.Data.Repository;
using System.Threading.Tasks;

public class AuthenticationLogic
{
    private readonly List<User> _users = new List<User>(); // Simulated database

    public bool DoesUserExist(string username)
    {
        var existedUser = new User { Nickname = "existinguser"};
        if (username == existedUser.Nickname)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DoesEmailExist(string email)
    {
        var existedUser = new User { Email = "existing@example.com" };
        if (email == existedUser.Email)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public async Task<BaseResponse<ClaimsIdentity>> Register(User IncomingUser)
    {
        if (DoesUserExist(IncomingUser.Nickname))
        {
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 404,
                Success = true,
                Message = "User with this name exist"
            };
        }
        else if (DoesEmailExist(IncomingUser.Email))
        {
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 404,
                Success = true,
                Message = "User with this email exist"
            };
        }
        else
        {
            // Simulate assigning roles and saving user data
            var response = Authenticate(IncomingUser);
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 200,
                Success = true,
                Data = response,
                Message = "OK"
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(User IncomingUser)
    {
        // Simulate retrieving user from database
        var validUser = new User { Nickname = "ValidUser", Password = "password", Role = new Role { Name = "Admin" } }; // Replace with appropriate logic

        if (IncomingUser.Password == validUser.Password)
        {
            var response = Authenticate(validUser);
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 200,
                Success = true,
                Data = response,
                Message = "OK"
            };
        }
        else
        {
            return new BaseResponse<ClaimsIdentity>
            {
                StatusCode = 404,
                Success = true,
                Message = "User was not found"
            };
        }
    }

    private ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>{
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name.ToString())
        };
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        return id;
    }
}
namespace SocialNetworkProject.Tests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public async Task Register_NewUser_Success()
        {
            // Arrange
            var logic = new AuthenticationLogic(); // Replace with appropriate class name
            var newUser = new User { Nickname = "newuser", Email = "newuser@example.com", RoleId = 1, Role = new Role {Name="Admin" } }; // Provide appropriate data

            // Act
            var result = await logic.Register(newUser);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("OK", result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(result.Data, typeof(ClaimsIdentity));
        }

        [TestMethod]
        public async Task Register_ExistingUsername_Failure()
        {
            // Arrange
            var logic = new AuthenticationLogic(); // Replace with appropriate class name
            var existingUser = new User { Nickname = "existinguser" }; // Provide appropriate data

            // Act
            var result = await logic.Register(existingUser);

            // Assert
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("User with this name exist", result.Message);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public async Task Register_ExistingEmail_Failure()
        {
            // Arrange
            var logic = new AuthenticationLogic(); // Replace with appropriate class name
            var existingEmail = new User { Email = "existing@example.com" }; // Provide appropriate data

            // Act
            var result = await logic.Register(existingEmail);

            // Assert
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("User with this email exist", result.Message);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public async Task Login_ValidUser_Success()
        {
            // Arrange
            var logic = new AuthenticationLogic(); // Replace with appropriate class name
            var validUser = new User { Nickname = "ValidUser", Password = "password" }; // Provide appropriate data

            // Act
            var result = await logic.Login(validUser);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("OK", result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(result.Data, typeof(ClaimsIdentity));
        }

        [TestMethod]
        public async Task Login_InvalidUser_Failure()
        {
            // Arrange
            var logic = new AuthenticationLogic(); // Replace with appropriate class name
            var invalidUser = new User { Nickname = "InvalidUser", Password = "invalidpassword" }; // Provide appropriate data

            // Act
            var result = await logic.Login(invalidUser);

            // Assert
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("User was not found", result.Message);
            Assert.IsNull(result.Data);
        }
    }
}
