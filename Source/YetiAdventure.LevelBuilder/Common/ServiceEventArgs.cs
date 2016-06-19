using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.LevelBuilder.Common
{
    public class ServiceEventArgs : EventArgs
    {
        public Type ServiceType { get; private set; }

        public ServiceEventArgs(Type type)
        {
            ServiceType = type;
        }
    }
}
