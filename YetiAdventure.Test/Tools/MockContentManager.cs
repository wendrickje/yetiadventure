using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Test.Tools
{
    public class MockContentManager : ContentManager
    {
        public MockContentManager() : base(new MockServiceProvider())
        {
        }
    }
}
