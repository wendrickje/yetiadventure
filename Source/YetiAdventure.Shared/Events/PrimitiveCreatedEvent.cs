using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Shared.Events
{
    /// <summary>
    /// Polygon Created event
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{YetiAdventure.Shared.Events.PrimitiveCreatedEventArgs}" />
    public class PrimitiveCreatedEvent : PubSubEvent<PrimitiveCreatedEventArgs>
    {

    }

    /// <summary>
    /// Primitive Created args
    /// </summary>
    public class PrimitiveCreatedEventArgs
    {
        /// <summary>
        /// newly created item
        /// </summary>
        public Primitive NewItem { get; set; }

    }
}
