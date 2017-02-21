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
using YetiAdventure.LevelBuilder.ViewModel;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.View
{
    /// <summary>
    /// Interaction logic for ToolboxView.xaml
    /// </summary>
    public partial class ToolBoxView : UserControl
    {
        public ToolBoxView()
        {
            InitializeComponent();
        }

        public ToolBoxView(ToolBoxViewModel vm) : this()
        {
            DataContext = vm;
        }
    }
}
