using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Shared;

namespace YetiAdventure.Shared.Interfaces
{
    public interface ILevelBuilderContext
    {
        void MovePrimitive(Primitive primitive, double x, double y);
    }
}
