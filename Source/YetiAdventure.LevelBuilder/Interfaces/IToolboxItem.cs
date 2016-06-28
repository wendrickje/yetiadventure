using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.Interfaces
{
    public interface IToolboxItem
    {
        string Tooltip { get; }

        string Caption { get; }

        ICommand Command { get; }

        IconType Icon { get; }

    }
}
