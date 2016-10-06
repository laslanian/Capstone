using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProject.Models.Services;

namespace CapstoneProject.Tests.Controllers
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void TestSendPinEmail()
        {
            EmailService es = new EmailService();
            es.SendGroupPin("leoaslanian13@hotmail.com", "1234");

        }
    }
}