using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using YetiAdventure.LevelBuilder.Controls;
using YetiAdventure.LevelBuilder.ViewModel;
using Prism.Regions;
using Microsoft.Practices.Unity;

namespace YetiAdventure.LevelBuilder.View
{
    /// <summary>
    /// Interaction logic for LevelCanvas.xaml
    /// </summary>
    public partial class LevelCanvasView : UserControl
    {
        IUnityContainer _container;
        LevelCanvasControl _gameControl;

        public LevelCanvasView()
        {
            InitializeComponent();
        }


        public LevelCanvasView(LevelCanvasViewModel vm, IUnityContainer container) : this()
        {
            DataContext = vm;
            _container = container;
            _gameControl = new LevelCanvasControl(container);
            _gameControl.Show();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Initialize();
        }

        void Initialize()
        {
            var panel = new System.Windows.Forms.Panel();
            var handle = panel.Handle;
            FormsHost.Child = panel;
            FormsHost.SizeChanged += (s, a) =>
            {
                _gameControl.ClientSize = new System.Drawing.Size((int)FormsHost.ActualWidth, (int)FormsHost.ActualHeight);
            };
            panel.Controls.Add(_gameControl);




        }


    }
}
