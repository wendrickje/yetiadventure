using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Interfaces;
using YetiAdventure.Shared.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Engine.Components.BuilderOperations
{
    /// <summary>
    /// transform operation
    /// </summary>
    public class TransformOperation : ToolOperationBase
    {
        private IEventAggregator _eventAggregator;
        private IPrimitiveManager _primitiveManager;

        private Primitive _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformOperation"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="primitiveManager">The primitive manager.</param>
        public TransformOperation(IEventAggregator eventAggregator, IPrimitiveManager primitiveManager)
        {
            _eventAggregator = eventAggregator;
            _primitiveManager = primitiveManager;
            //_eventAggregator.GetEvent<SelectionChangedEvent>().Subscribe(HandleSelectionChangedEvent);
        }

        ///// <summary>
        ///// Handles the selection changed event.
        ///// </summary>
        ///// <param name="obj">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        //private void HandleSelectionChangedEvent(SelectionChangedEventArgs obj)
        //{
        //    _target = obj.NewItem;
        //}

        bool _isOperating;

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="args"></param>
        public override void Update(ToolOperationArgs args)
        {
            base.Update(args);
            if (args.MouseState.LeftButton == OpenTK.Input.ButtonState.Released)
            {
                _isOperating = false;
            }

            //get selected item
            if (args.MouseState.LeftButton == OpenTK.Input.ButtonState.Pressed)
            {
                _target = _primitiveManager.GetPrimitiveUnderPoint(args.MousePoint);
                //calculate the top-left of the prim using mouse point as a reference
                var point = args.MousePoint;
                var reference = new Shared.Common.Point(point.X, point.Y);
                _isOperating = _target != null;
            }

            if (!_isOperating) return;


            //point will be somewhere inside the prim
            //when draging around prim update position
            

            _primitiveManager.MovePrimitive(_target, args.MousePoint);

            

            
        }


        
    }
}
