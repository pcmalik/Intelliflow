using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSvc1.Models;
using WebSvc1.Controllers;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Threading;
using System.Net;
using WebSvc1.Repositories;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebSvc1.Test
{
    [TestClass]
    public class RemunerationControllerTests
    {
        [TestMethod]
        public async Task Test_Post_When_Data_Saved_Then_Returns_Accepted()
        {
            var bonusRepository = new Mock<IBonusRepository>();
            var bonusRecipients = new Mock<BonusRecipients>();

            bonusRepository.Setup(x => x.SaveAsync(It.IsAny<List<EmployeeBonus>>())).ReturnsAsync(true);

            var remunerationController = new RemunerationController(bonusRepository.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            var result = await remunerationController.Post(bonusRecipients.Object);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);

        }

        [TestMethod]
        public async Task Test_Post_When_Data_Not_Saved_Then_Returns_BadRequest()
        {
            var bonusRepository = new Mock<IBonusRepository>();
            var bonusRecipients = new Mock<BonusRecipients>();

            bonusRepository.Setup(x => x.SaveAsync(It.IsAny<List<EmployeeBonus>>())).ReturnsAsync(false);

            var remunerationController = new RemunerationController(bonusRepository.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            var result = await remunerationController.Post(bonusRecipients.Object);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task Test_Post_When_Model_Is_Invalid_Then_Returns_BadRequest()
        {
            var bonusRepository = new Mock<IBonusRepository>();
            var bonusRecipients = new Mock<BonusRecipients>();

            var remunerationController = new RemunerationController(bonusRepository.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            remunerationController.ModelState.AddModelError("key", "error message");

            var result = await remunerationController.Post(bonusRecipients.Object);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task Test_Post_When_Model_Is_Null_Then_Returns_BadRequest()
        {
            var bonusRepository = new Mock<IBonusRepository>();
            var bonusRecipients = new Mock<BonusRecipients>();

            var remunerationController = new RemunerationController(bonusRepository.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };
            var result = await remunerationController.Post(null);
            var response = await result.ExecuteAsync(CancellationToken.None);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

    }
}
