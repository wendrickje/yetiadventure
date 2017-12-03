using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Components;
using YetiAdventure.Engine.Components.BuilderOperations;
using YetiAdventure.Engine.Interfaces;
using YetiAdventure.Engine.Levels;
using YetiAdventure.LevelBuilder.ViewModels;
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
        public void Operation_ValidPrimitive_Move_ValidPosition()
        {
            var world = new Mock<World>(new Microsoft.Xna.Framework.Vector2());
            var primitive = new Primitive(new Shared.Common.Rectangle(), new Body(world.Object));
            var expected = new Shared.Common.Point(20, 20);

            var eventagg = new Mock<IEventAggregator>();
            eventagg.Setup(mock => mock.GetEvent<PrimitiveCreatedEvent>()).Returns(new PrimitiveCreatedEvent());
            var service = new PrimitiveManager(eventagg.Object);
            service.MovePrimitive(primitive, expected);
            Assert.AreEqual(expected, primitive.Bounds.UpperLeft());
        }


        [TestMethod]
        public void Operation_ValidPrimitive_Select()
        {
            var world = new Mock<World>(new Microsoft.Xna.Framework.Vector2());
            var eventagg = new Mock<IEventAggregator>();
            eventagg.Setup(mock => mock.GetEvent<PrimitiveCreatedEvent>()).Returns(new PrimitiveCreatedEvent());
            var service = new PrimitiveManager(eventagg.Object);
            var expected = new Primitive(new Shared.Common.Rectangle(100, 100, 0, 0), new Body(world.Object)) { };
            service.Primitives.Add(Guid.NewGuid(), expected);
            var operation = new SelectionOperation(eventagg.Object, service);
            var actual = operation.GetOperationTarget(new Shared.Common.Point(50, 50));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operation_ValidPrimitive_Transform_SetTarget()
        {
            //var world = new Mock<World>(new Microsoft.Xna.Framework.Vector2());
            //var eventagg = new Mock<IEventAggregator>();
            //eventagg.Setup(mock => mock.GetEvent<PrimitiveCreatedEvent>()).Returns(new PrimitiveCreatedEvent());
            //var service = new PrimitiveManager(eventagg.Object);
            //var expected = new Primitive(new Shared.Common.Rectangle(100, 100, 0, 0), new Body(world.Object)) { };
            //service.Primitives.Add(Guid.NewGuid(), expected);
            //var operation = new TransformOperation(eventagg.Object, service);
            //var args = new ToolOperationArgs();
            //operation.Update(args);
            
        }


        [TestMethod]
        public void ActiveToolBoxItem_Activation()
        {
            var eventagg = new Mock<IEventAggregator>();
            eventagg.Setup(mock => mock.GetEvent<SelectionChangedEvent>()).Returns(new SelectionChangedEvent());
            var container = new Mock<IUnityContainer>();
            var service = new Mock<ILevelBuilderService>();
            var toolbox = new ToolBoxViewModel(eventagg.Object, container.Object, service.Object);
            var expected = toolbox.ToolBoxItems.Last();
            toolbox.ActiveToolBoxItem = expected;
            Assert.AreEqual(expected, toolbox.ActiveToolBoxItem);
            
        }


        [TestMethod]
        public void PrimitiveManager_EventPublish_OnAddNewPolygon()
        {
            var primEvent = new PrimitiveCreatedEvent();
            var world = new Mock<World>(new Microsoft.Xna.Framework.Vector2());
            var expectedPrim = new Primitive(new Shared.Common.Rectangle(5,6,7,898), new Body(world.Object));
            Primitive actualPrim = null;
            Action<PrimitiveCreatedEventArgs> handler = arg => { actualPrim = arg.NewItem; };
            primEvent.Subscribe(handler);
            
            var eventagg = new Mock<IEventAggregator>();
            eventagg.Setup(mock => mock.GetEvent<PrimitiveCreatedEvent>()).Returns(primEvent);

            var primMgr = new Mock<IPrimitiveManager>();

            var polygonOperation = new PolygonOperation(eventagg.Object, primMgr.Object);
            polygonOperation.AddPrimitive(expectedPrim);
            Assert.AreEqual(expectedPrim, actualPrim);
        }


    }
}
