using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Test.Tools
{
    public class MockServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return null;
        }
    }
}
