using Microsoft.Xna.Framework;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Interfaces;

namespace YetiAdventure.Engine.Components.BuilderOperations
{
    /// <summary>
    /// The ruler tool.
    /// </summary>
    public class RulerOperation : ToolOperationBase
    {
        private Vector2? _startPosition;
        private Vector2 _currentPosition;

        /// <summary>
        /// The ruler constructor.
        /// </summary>
        public RulerOperation()
        {
            _startPosition = null;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update(ToolOperationArgs args)
        {
            _currentPosition = new Vector2(args.MouseState.X, args.MouseState.Y);

            if (_startPosition == null)
            {
                // Start point hasn't been selected yet.
                if (args.IsLeftMouseButtonClicked())
                {
                    _startPosition = new Vector2(args.MouseState.X, args.MouseState.Y);
                }
            }
            else
            {
                // The user has selected a start position. Only reset the state when the mouselick is released.
                if (args.MouseState.LeftButton == OpenTK.Input.ButtonState.Released)
                {
                    _startPosition = null;
                }
            }
        }
        /// <summary>
        /// Draws the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Draw(ToolOperationArgs args)
        {
            if (_startPosition != null)
            {
                // Test constants for tuning.
                float tickLength = 2.0f;
                int numMarks = 10;

                Vector2 rulerDispacement = _currentPosition - _startPosition.Value;
                float worldDistance = rulerDispacement.Length();

                // Create a vector perpendicular to the ruler to draw tick marks.
                float tempX = rulerDispacement.X;
                Vector2 perpendicular = new Vector2(rulerDispacement.Y, -tempX);
                perpendicular.Normalize();

                // Draw the ruler line from the start to end position.
                args.SpriteBatch.DrawLine(_startPosition.Value, _currentPosition, Color.Blue);

                // Draw tick marks along the line.
                rulerDispacement.Normalize();
                Vector2 tickStart = _startPosition.Value;
                for (int markIndex = 0; markIndex <= numMarks; ++markIndex)
                {
                    Vector2 tickEnd = tickStart + (perpendicular * tickLength);
                    args.SpriteBatch.DrawLine(tickStart, tickEnd, Color.Blue);
                }
            }
        }
    }
}
