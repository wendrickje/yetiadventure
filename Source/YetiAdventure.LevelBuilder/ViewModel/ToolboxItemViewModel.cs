using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YetiAdventure.LevelBuilder.Interfaces;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.ViewModel
{
    public class ToolboxItemViewModel : CoreViewModel, IToolboxItem
    {
        /// <summary>
        /// ToolboxItemViewModel Constructor
        /// </summary>
        /// <param name="caption">Descriptive caption</param>
        /// <param name="tooltip">Additional help text</param>
        /// <param name="icon">Icon to use as display</param>
        /// <param name="command">Command to execute</param>
        public ToolboxItemViewModel(string caption, string tooltip, IconType icon, ICommand command)
        {
            Caption = caption;
            Tooltip = tooltip;
            Command = command;
            Icon = icon;
        }

        public string Caption
        {
            get;
            private set;
        }

        public ICommand Command
        {
            get;
            private set;
        }

        public IconType Icon
        {
            get;
            private set;
        }

        public string Tooltip
        {
            get;
            private set;
        }
    }
}
