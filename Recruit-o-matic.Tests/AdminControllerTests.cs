using System;
using Recruit_o_matic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruit_o_matic.Controllers;
using System.Web.Mvc;

namespace Recruit_o_matic.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void HomeMethodUsesConventionToChooseView()
        {
            //Arrange
            var controller = new AdminController();

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
            
        }
    }
}
