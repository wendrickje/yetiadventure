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
using YetiAdventure.LevelBuilder.ViewModels;

namespace YetiAdventure.LevelBuilder.Views
{
    /// <summary>
    /// Interaction logic for PropertiesView.xaml
    /// </summary>
    public partial class PropertiesView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesView"/> class.
        /// </summary>
        public PropertiesView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesView"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public PropertiesView(PropertiesViewModel vm) : this()
        {
            DataContext = vm;
        }
    }
}
