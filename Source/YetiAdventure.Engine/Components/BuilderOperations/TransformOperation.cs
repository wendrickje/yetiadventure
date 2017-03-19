using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
        private List<Vector2> _handles = new List<Vector2>();

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

            //when operation completed reset
            if (args.MouseState.LeftButton == OpenTK.Input.ButtonState.Released)
            {
                _isOperating = false;
            }

            //get selected item
            //target should only be set once
            if (args.MouseState.LeftButton == OpenTK.Input.ButtonState.Pressed && !_isOperating)
            {
                _target = _primitiveManager.GetPrimitiveUnderPoint(args.MousePoint);
                _isOperating = _target != null;
            }

            if (!_isOperating) return;

            _handles = new List<Vector2>();
            var bounds = _target.Bounds;
            var xRect = bounds.ConvertSharedRectangleToRectangle();

            _handles.Add(new Vector2(xRect.X, xRect.Y));
            _handles.Add(new Vector2(xRect.X + (xRect.Width / 2), xRect.Y));
            _handles.Add(new Vector2(xRect.X + xRect.Width, xRect.Y));
            _handles.Add(new Vector2(xRect.X, xRect.Y + xRect.Height));
            _handles.Add(new Vector2(xRect.X + (xRect.Width / 2), xRect.Y + xRect.Height));
            _handles.Add(new Vector2(xRect.X + xRect.Width, xRect.Y + xRect.Height));
            _handles.Add(new Vector2(xRect.X, xRect.Y + (xRect.Height / 2)));
            _handles.Add(new Vector2(xRect.X + xRect.Width, xRect.Y + (xRect.Height / 2)));

            //calculate the top-left of the prim using mouse point as a reference
            var point = args.MousePoint;
            var topleft = _target.Bounds.UpperLeft();
            var delta = topleft - point;
            var destination = args.MousePoint - delta;

            //determine if resize or drag
            var activeHandle = _handles.Select(v => new Rectangle?(new Rectangle(v.ToPoint(), new Point(1)))).FirstOrDefault(r => r.Value.Contains(args.MousePoint.ConvertToVector2()));
            if (activeHandle != null)
            {
                _primitiveManager.Resize(_target, destination, activeHandle.Value.ConvertToSharedRectangle());
            }
            else
            {
                //point will be somewhere inside the prim
                //when draging around prim update position
                _primitiveManager.MovePrimitive(_target, destination);
            }




        }

        /// <summary>
        /// Draws the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Draw(ToolOperationArgs args)
        {
            base.Draw(args);
            //draw bounding box and bounding tools
            if (_target == null) return;
            var spriteBatch = args.SpriteBatch;
            var bounds = _target.Bounds;
            var xRect = bounds.ConvertSharedRectangleToRectangle();
            //go through common code whenever possible _primitiveManager
            spriteBatch.DrawRectangle(xRect, SelectedColor, 3f);
            //draw 8 outside stretch indicators
            //draw 1 center point 
            
            foreach (var vector in _handles)
                spriteBatch.DrawCircle(vector, 0.25f, 10, HandleColor, 3);

        }

    }
}
