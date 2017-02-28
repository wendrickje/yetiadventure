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
using Microsoft.Practices.Unity;
using OpenTK.Platform;
using Prism.Regions;
using YetiAdventure.LevelBuilder.ViewModel;

namespace YetiAdventure.LevelBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowInfo
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public MainWindow(MainWindowViewModel vm) : this()
        {
            DataContext = vm;

        }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        public IntPtr Handle { get { return this.Handle; } }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>       
        public void Dispose()
        {
            
        }
    }
}
