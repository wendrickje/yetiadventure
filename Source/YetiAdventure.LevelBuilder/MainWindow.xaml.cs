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
using YetiAdventure.LevelBuilder.ViewModel;

namespace YetiAdventure.LevelBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BuilderFrameViewModel();
        }


        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var grid = sender as Grid;
            var dx = grid.DataContext as BuilderFrameViewModel;
            //e.CanExecute = dx.NewCommand.CanExecute(null);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            var dx = grid.DataContext as BuilderFrameViewModel;
            //dx.NewCommand.Execute(null);

        }
    }
}
