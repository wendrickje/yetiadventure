using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Regions;
using YetiAdventure.LevelBuilder.ViewModels;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.Views
{
    /// <summary>
    /// Interaction logic for ToolboxView.xaml
    /// </summary>
    public partial class ToolBoxView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBoxView"/> class.
        /// </summary>
        public ToolBoxView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBoxView"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public ToolBoxView(ToolBoxViewModel vm) : this()
        {
            DataContext = vm;
        }
    }
}
