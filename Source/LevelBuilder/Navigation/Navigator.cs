using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LevelBuilder.Navigation
{
    public class Navigator : FrameworkElement
    {


        static Navigator Instance;

        static Navigator()
        {
            Instance = new Navigator();
        }

        public static NavigationControl GetNavigationFrame(DependencyObject obj)
        {
            return (NavigationControl)obj.GetValue(NavigationFrameProperty);
        }

        public static void SetNavigationFrame(DependencyObject obj, NavigationControl value)
        {
            obj.SetValue(NavigationFrameProperty, value);
        }

        // Using a DependencyProperty as the backing store for NavigationFrame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationFrameProperty =
            DependencyProperty.RegisterAttached("NavigationFrame", typeof(NavigationControl), typeof(Navigator), new PropertyMetadata(null, OnNavigationFrameChanged));

        private static void OnNavigationFrameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nav = d as Navigator;
            if (Navigator.GetNavigationFrame(Instance) == e.NewValue) return;
            Instance.SetValue(Navigator.NavigationFrameProperty, e.NewValue);
        }

        public static void Navigate(INavigationData data)
        {
            var nav = Navigator.GetNavigationFrame(Instance);
            NavigationCommands.GoToPage.Execute(data, nav);
        }


    }
}
