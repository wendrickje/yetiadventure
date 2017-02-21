using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace YetiAdventure.LevelBuilder.Controls
{
    /// <summary>
    /// item selector
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Primitives.Selector" />
    public class ToolBox : Selector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBox"/> class.
        /// </summary>
        public ToolBox()
        {
            ItemsPanel = new System.Windows.Controls.ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel)));
            ItemContainerGenerator.StatusChanged += OnItemContainerGeneratorStatusChanged;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
        }

        /// <summary>
        /// Called when [item container generator status changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnItemContainerGeneratorStatusChanged(object sender, EventArgs e)
        {
            //setup init selection
            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated || SelectedItem == null) return;

            PerformOnSelectionChanged(SelectedItem);
        }



        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <returns>
        /// The element that is used to display the given item.
        /// </returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var button = new RadioButtonTool()
            {
                Margin = new Thickness(2),
                Width = 48,
            };
            button.SetBinding(RadioButtonTool.CommandProperty, CommandMemberPath);
            button.Checked += OnToggleChecked;
            return button;
        }

        /// <summary>
        /// State handler - used when user interaction changed state of item
        /// </summary>
        bool _updatingByUserInteraction = false;

        /// <summary>
        /// State handler - used when data binding changed state of item
        /// </summary>
        bool _updatingBySelectionChangedInteraction = false;


        /// <summary>
        /// Called when [toggle checked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnToggleChecked(object sender, RoutedEventArgs e)
        {
            if (_updatingBySelectionChangedInteraction) return;
            ResetCheckedItems();
            var data = ItemContainerGenerator.ItemFromContainer(sender as DependencyObject);
            _updatingByUserInteraction = true;
            SelectedItem = data;
            _updatingByUserInteraction = false;
            
        }



        /// <summary>
        /// Called when the selection changes.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (e.AddedItems.Count != 1) return;
            var data = e.AddedItems[0];
            PerformOnSelectionChanged(data);

        }
        private void PerformOnSelectionChanged(object data)
        {
            if (_updatingByUserInteraction) return;

            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return;

            _updatingBySelectionChangedInteraction = true;

            //reset
            ResetCheckedItems();
            var button = ItemContainerGenerator.ContainerFromItem(data) as RadioButtonTool;

            button.IsChecked = true;
            _updatingBySelectionChangedInteraction = false;
        }

        /// <summary>
        /// Resets the checked items.
        /// </summary>
        private void ResetCheckedItems()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = ItemContainerGenerator.ContainerFromIndex(i) as RadioButtonTool;
                item.IsChecked = false;

            }
        }

        /// <summary>
        /// Gets or sets the command member path.
        /// </summary>
        /// <value>
        /// The command member path.
        /// </value>
        public string CommandMemberPath { get; set; }
        
    }
}
