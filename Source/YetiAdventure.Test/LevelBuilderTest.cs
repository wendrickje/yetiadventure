using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;
using YetiAdventure.Engine.Levels;
using YetiAdventure.LevelBuilder.ViewModel;
using YetiAdventure.Shared;
using YetiAdventure.Shared.Events;
using YetiAdventure.Shared.Interfaces;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Test
{
    [TestClass]
    public class LevelBuilderTest
    {
        //given_when_then
        //Arrange
        //Act
        //Assert

        [TestMethod]
        public void ValidPrimitive_Move_ValidPosition()
        {
            var primitive = new Primitive(new Shared.Common.Rectangle());
            var expected = new Shared.Common.Point(20, 20);
            var engineStub = new Mock<IEngineProvider>();
            var service = new Mock<ILevelBuilderService>(engineStub);
            //service.Setup((mock) => mock.MovePrimitive(primitive, expected));
            Assert.AreEqual(expected, primitive.Bounds.UpperLeft());
        }


        [TestMethod]
        public void ActiveToolBoxItem_Activation()
        {
            var eventagg = new Mock<IEventAggregator>();
            eventagg.Setup(mock => mock.GetEvent<SelectionChangedEvent>()).Returns(new SelectionChangedEvent());
            var container = new Mock<IUnityContainer>();
            var engineStub = new Mock<IEngineProvider>();
            var service = new Mock<ILevelBuilderService>(engineStub);
            var toolbox = new ToolBoxViewModel(eventagg.Object, container.Object, service.Object);
            var expected = toolbox.ToolBoxItems.Last();
            toolbox.ActiveToolBoxItem = expected;
            Assert.AreEqual(expected, toolbox.ActiveToolBoxItem);
            
        }
    }
}
