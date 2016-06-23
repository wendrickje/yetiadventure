using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.LevelBuilder.Interfaces
{
    public interface INavigationData
    {
        Type PageType { get; }
        object DataContext { get; }
    }
}
