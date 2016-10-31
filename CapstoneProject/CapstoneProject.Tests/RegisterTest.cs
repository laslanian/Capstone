using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProject.Models.Services;

namespace CapstoneProject.Tests
{
    [TestClass]
    public class RegisterTest
    {
        private UserAccountService uac = new UserAccountService;
        [TestMethod]
        public void addTest()
        {
            User s = new User();
            s.FirstName = "First";
            s.LastName = "Last";
            s.PhoneNumber = "9871237560";
            s.Password = "Test";
            uac.RegisterStudent((Student) s);
        }
    }
}
