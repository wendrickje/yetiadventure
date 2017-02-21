using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Shared.Events
{
    public class SelectionChangedEvent : PubSubEvent<SelectionChangedEventArgs>
    {
    }

    public class SelectionChangedEventArgs
    {
        /// <summary>
        /// newly selected item
        /// </summary>
        public Primitive NewItem { get; set; }

    }


}
