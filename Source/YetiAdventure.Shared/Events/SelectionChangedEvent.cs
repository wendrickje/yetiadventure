﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Shared.Events
{
    /// <summary>
    /// selection changed event
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{YetiAdventure.Shared.Events.SelectionChangedEventArgs}" />
    public class SelectionChangedEvent : PubSubEvent<SelectionChangedEventArgs>
    {
    }

    /// <summary>
    /// selection changed args
    /// </summary>
    public class SelectionChangedEventArgs
    {
        /// <summary>
        /// newly selected item
        /// </summary>
        public Primitive NewItem { get; set; }

    }
}