using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Prism.Events;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Interfaces;
using YetiAdventure.Shared.Common;
using YetiAdventure.Shared.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Engine.Components.BuilderOperations
{
    /// <summary>
    /// selection operation
    /// </summary>
    /// <seealso cref="YetiAdventure.Engine.Common.ToolOperationBase" />
    public class SelectionOperation : ToolOperationBase
    {
        private List<Primitive> _operationTargets = new List<Primitive>();
        private IEventAggregator _eventAggregator;
        private IPrimitiveManager _primitiveManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionOperation"/> class.
        /// </summary>
        public SelectionOperation(IEventAggregator eventAggregator, IPrimitiveManager primitiveManager)
        {
            _eventAggregator = eventAggregator;
            _primitiveManager = primitiveManager;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="args"></param>
        public override void Update(ToolOperationArgs args)
        {
            base.Update(args);

            var position = args.MousePoint;
            if(args.IsLeftMouseButtonClicked())
            {
                var target = GetOperationTarget(args.MousePoint);
                if(target != null)
                {
                    AddOperationTarget(target);
                }
            }
        }

        /// <summary>
        /// Draws the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Draw(ToolOperationArgs args)
        {
            base.Draw(args);
            var spriteBatch = args.SpriteBatch;
            foreach(var prim in _operationTargets)
            {
                //cache the binding box or not?
                var box = prim.Bounds.ConvertSharedRectangleToRectangle();
#if Debug
                var boxDimensions = String.Format("X {0}, Y {1} | H {2} W {3}", box.X, box.Y, box.Height, box.Width);
                spriteBatch.DrawString(args.SpriteFont, boxDimensions, new Vector2(box.Center.X, box.Center.Y), Microsoft.Xna.Framework.Color.White);
#endif
                spriteBatch.DrawRectangle(box, Microsoft.Xna.Framework.Color.ForestGreen);

               

            }

        }

        internal void AddOperationTarget(Primitive target)
        {
            if(_operationTargets.Contains(target)) return;
            _operationTargets.Add(target);
            _eventAggregator.GetEvent<SelectionChangedEvent>().Publish(new SelectionChangedEventArgs() { NewItem = target });
            
        }

        internal Primitive GetOperationTarget(Shared.Common.Point mousePoint)
        {

            //need to go through list of prims and find the one under the mouse
            //the pass it to this operation
            var prim = _primitiveManager.GetPrimitiveUnderPoint(mousePoint);
            return prim;
        }
    }
}
