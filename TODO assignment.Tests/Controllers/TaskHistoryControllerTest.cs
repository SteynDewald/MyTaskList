using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TODO_assignment;
using TODO_assignment.Controllers;

namespace TODO_assignment.Tests.Controllers
{
    [TestClass]
    public class TaskHistoryControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            TaskHistoryController controller = new TaskHistoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Index()
        {
            // Arrange
            TaskHistoryController controller = new TaskHistoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            TaskHistoryController controller = new TaskHistoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }




    }
}
