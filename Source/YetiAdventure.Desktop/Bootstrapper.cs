using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace YetiAdventure.Desktop
{
    /// <summary>
    /// application bootstrapper
    /// </summary>
    public class Bootstrapper 
    {
        IEventAggregator _eventAggregator;
        IUnityContainer _container;


        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            _container = new UnityContainer();
            _eventAggregator = new EventAggregator();
        }

        /// <summary>
        /// Initalizes this instance.
        /// </summary>
        public void Initalize()
        {
            _container.RegisterInstance(_eventAggregator);
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

    }
}
