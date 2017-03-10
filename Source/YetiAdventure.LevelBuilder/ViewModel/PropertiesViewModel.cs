using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using YetiAdventure.LevelBuilder.Common;
using YetiAdventure.Shared.Events;


namespace YetiAdventure.LevelBuilder.ViewModel
{
    /// <summary>
    /// properties view model
    /// </summary>
    public class PropertiesViewModel : CoreViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PropertiesViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<SelectionChangedEvent>().Subscribe(OnPrimitiveSelectionChangedEventHandler);
        }

        /// <summary>
        /// Raises the <see cref="E:PrimitiveSelectionChangedEventHandler" /> event.
        /// </summary>
        /// <param name="obj">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnPrimitiveSelectionChangedEventHandler(SelectionChangedEventArgs obj)
        {
            Target = obj.NewItem;
        }

        public const string RegionName = "PropertiesViewModel";

        private ICommand _saveCommand;

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand { get { return _saveCommand ?? (_saveCommand = new RelayCommand(DoSave, CanSave)); } }

        /// <summary>
        /// Determines whether this instance can save the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if this instance can save the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool CanSave(object obj)
        {
            return true;
        }

        /// <summary>
        /// Does the save.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void DoSave(object obj)
        {
        }

        private object _target;

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public object Target
        {
            get { return _target; }
            set { SetProperty(ref _target, value); }
        }

    }
}
