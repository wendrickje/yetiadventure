using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Test.Tools
{
    public class MockGraphicsDeviceManager : GraphicsDeviceManager
    {
        public MockGraphicsDeviceManager() : base(new MockGame())
        {
            CreateDevice();
        }
    }
}
