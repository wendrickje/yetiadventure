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

namespace LevelBuilder.Navigation
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
            CommandManager.RegisterClassCommandBinding(typeof(NavigationControl), new CommandBinding(NavigationCommands.GoToPage, GoToPageExecuted));
        }

        private void GoToPageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var navData = e.Parameter as INavigationData;
            var resolver = new PageResolver() { PageType = navData.PageType };
            var page = resolver.ProvideValue(null) as Page;
            var datacontext = navData.DataContext;
            page.DataContext = datacontext;
            navFrame.Navigate(page);
        }


        

    }
}
