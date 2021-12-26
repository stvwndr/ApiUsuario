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
    public class UserDomainTest
    {
        private readonly Mock<IUserProvider> _userRepositoryMock;
        private readonly Mock<IHashProvider> _hashProviderMock;
        private readonly UserDomain _domain;



        private UserRequest userRequest = new()
        {
            Email = "johndoe@hotmail.com",
            Name = "John Doe",
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

        public UserDomainTest()
        {
            _userRepositoryMock = new Mock<IUserProvider>();
            _hashProviderMock = new Mock<IHashProvider>();
            _domain = new UserDomain(_userRepositoryMock.Object, _hashProviderMock.Object);
        }


        #region CreateUser()
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateUser_User_Null_Test_Failed()
        {
            _userRepositoryMock.Setup(u => u.CreateUser(user)).Returns(null as User);
            _hashProviderMock.Setup(h => h.GenerateHash(userRequest.Password)).Returns(user.Password);
            _domain.CreateUser(userRequest);
        }

        [TestMethod]
        public void CreateUser_Test_Success()
        {
            _userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(user);
            _hashProviderMock.Setup(h => h.GenerateHash(userRequest.Password)).Returns(user.Password);
            _domain.CreateUser(userRequest);
        }

        #endregion
    }
}
