using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TODO_assignment;
using TODO_assignment.Controllers;
using TODO_assignment.Models;
using System.Security.Principal;
using System.Threading;

namespace TODO_assignment.Tests.Controllers
{
    [TestClass]
    public class TaskItemsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            TaskItemsController controller = new TaskItemsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            TaskItemsController controller = new TaskItemsController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            TaskItemsController controller = new TaskItemsController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        
    }
}
