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
    public class ToolBoxItemViewModel : CoreViewModel, IToolboxItem
    {
        /// <summary>
        /// ToolboxItemViewModel Constructor
        /// </summary>
        /// <param name="caption">Descriptive caption</param>
        /// <param name="tooltip">Additional help text</param>
        /// <param name="icon">Icon to use as display</param>
        /// <param name="command">Command to execute</param>
        public ToolBoxItemViewModel(string caption, string tooltip, IconType icon, ICommand command)
        {
            Caption = caption;
            ToolTip = tooltip;
            Command = command;
            Icon = icon;
        }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public IconType Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the tooltip.
        /// </summary>
        /// <value>
        /// The tooltip.
        /// </value>
        public string ToolTip
        {
            get;
            private set;
        }
        

    }
}
