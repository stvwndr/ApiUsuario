using Domain.Services;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Test
{
    [TestClass]
    public class LoginDomainTest
    {
        private readonly Mock<IUserProvider> _userRepositoryMock;
        private readonly Mock<IHashProvider> _hashProviderMock;
        private readonly LoginDomain _domain;
        private LoginRequest loginRequest = new()
        {
            Email = "johndoe@hotmail.com",
            Password = "randompassword"
        };
        User user = new()
        {

            Id = 1,
            Email = "johndoe@hotmail.com",
            Name = "John Doe",
            Password = "689307D2FC53AF0FB941BC1BB42737CE4F3EF540",
            Created = DateTime.Now.ToUniversalTime(),
            Modified = DateTime.Now.ToUniversalTime(),
            LastLogin = DateTime.Now.ToUniversalTime(),

        };

        public LoginDomainTest()
        {
            _userRepositoryMock = new Mock<IUserProvider>();
            _hashProviderMock = new Mock<IHashProvider>();
            _domain = new LoginDomain(_userRepositoryMock.Object, _hashProviderMock.Object);
        }

        #region Login()

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Login_User_Null_Test_Failed()
        {
            _userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(null as User);
            _domain.Login(loginRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Login_Password_Different_Test_Failed()
        {
            _userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(user);
            _hashProviderMock.Setup(h => h.GenerateHash(It.IsAny<string>())).Returns("789307D2FC53AF0FB941BC1BB42737CE4F3EF540");

            _domain.Login(loginRequest);
        }

        [TestMethod]
        public void Login_Test_Success()
        {
            _userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(user);
            _hashProviderMock.Setup(h => h.GenerateHash(It.IsAny<string>())).Returns(user.Password);

            _domain.Login(loginRequest);
        }

        #endregion
    }
}
