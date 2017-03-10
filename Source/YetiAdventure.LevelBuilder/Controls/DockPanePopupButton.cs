using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;
using System.Windows.Controls;
using System.Windows.Data;
using YetiAdventure.LevelBuilder.Converters;
using YetiAdventure.LevelBuilder.Common;
using Xceed.Wpf.AvalonDock;

namespace YetiAdventure.LevelBuilder.Controls
{
    /// <summary>
    /// custom button for dockpanes
    /// </summary>
    /// <seealso cref="ToggleButton" />
    [TemplatePart(Name = "PART_popout", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_popin", Type = typeof(FrameworkElement))]
    public class DockPanePopupButton : ToggleButton
    {
        /// <summary>
        /// Initializes the <see cref="DockPanePopupButton"/> class.
        /// </summary>
        static DockPanePopupButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockPanePopupButton), new FrameworkPropertyMetadata(typeof(DockPanePopupButton)));
        }


        FrameworkElement _popout;
        FrameworkElement _popin;
        Window _popup;

        /// <summary>
        /// Initializes a new instance of the <see cref="DockPanePopupButton"/> class.
        /// </summary>
        public DockPanePopupButton() {  }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _popout = GetTemplateChild("PART_popout") as FrameworkElement;
            _popin = GetTemplateChild("PART_popin") as FrameworkElement;
            Command = new RelayCommand(DoChangeState);
            InitializeBindings();

        }


        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        internal void InitializeBindings()
        {
            var visbinding = new Binding()
            {
                Source = this,
                Path = new PropertyPath(IsCheckedProperty),
                Converter = new Converters.BooleanToVisibilityConverter()
            };
            var invertvisbinding = new Binding()
            {
                Source = this,
                Path = new PropertyPath(IsCheckedProperty),
                Converter = new InverseBooleanToVisibilityConverter()
            };

            _popin.SetBinding(VisibilityProperty, visbinding);
            _popout.SetBinding(VisibilityProperty, invertvisbinding);

        }

        /// <summary>
        /// Does the state of the change.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void DoChangeState(object obj)
        {
            var isFloating = IsChecked.Value == true;
            ChangeHostState(isFloating);
        }

        /// <summary>
        /// Changes the state of the host.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void ChangeHostState(bool state)
        {
            if (Host == null) return;
            //when state is true float when false dock
            var action = state ? (Action)(() => Host.Float()) : (Action)(() => Host.Dock());
            action();
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public LayoutAnchorable Host
        {
            get { return (LayoutAnchorable)GetValue(HostProperty); }
            set { SetValue(HostProperty, value); }
        }

        /// <summary>
        /// The host property
        /// </summary>
        public static readonly DependencyProperty HostProperty =
            DependencyProperty.Register("Host", typeof(LayoutAnchorable), typeof(DockPanePopupButton), new PropertyMetadata(null));


    }
    
}
