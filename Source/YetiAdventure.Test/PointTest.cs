using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YetiAdventure.Shared.Common;

namespace YetiAdventure.Test
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        public void Operator_Greater_Valid()
        {
            var first = new Point(100,100);
            var second = new Point(0,0);
            Assert.IsTrue(first > second);
            Assert.IsTrue(first >= second);
        }

        [TestMethod]
        public void Operator_Greater_Invalid()
        {
            var first = new Point(100, 100);
            var second = new Point(0, 0);
            Assert.IsFalse(second > first);
            Assert.IsFalse(second >= first);
        }

        [TestMethod]
        public void Operator_Less_Valid()
        {
            var first = new Point(0, 0);
            var second = new Point(100, 100);
            Assert.IsTrue(first < second);
            Assert.IsTrue(first <= second);
        }
        
        [TestMethod]
        public void Operator_Less_Invalid()
        {
            var first = new Point(0, 0);
            var second = new Point(100, 100);
            Assert.IsFalse(second < first);
            Assert.IsFalse(second <= first);
        }


        [TestMethod]
        public void Operator_GreaterOrEqual_Valid()
        {
            var first = new Point(100, 100);
            var second = new Point(100, -100);
            Assert.IsTrue(first >= second);
        }


        [TestMethod]
        public void Operator_LessOrEqual_Valid()
        {
            var first = new Point(100, -100);
            var second = new Point(100, 100);
            Assert.IsTrue(first <= second);
        }

        [TestMethod]
        public void Rectangle_PointContains_Valid()
        {
            var rect = new Rectangle(100, 100, 0, 0);
            var point = new Point(50, 50);
            Assert.IsTrue(rect.Contains(point));
        }

        [TestMethod]
        public void Rectangle_PointContains_Invalid()
        {
            var rect = new Rectangle(100,100,0,0);
            var point = new Point(800,0);
            Assert.IsFalse(rect.Contains(point));
        }

    }
}
