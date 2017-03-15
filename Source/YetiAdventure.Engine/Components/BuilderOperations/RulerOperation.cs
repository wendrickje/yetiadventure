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
        private string _lastLengthString;

        // Constants for visual tuning.
        private const float kTickLength = 1.0f;
        private const float kDistanceBetweenTickMarks = 1.5f;
        private const float kLabelScale = 0.05f;

        /// <summary>
        /// The ruler constructor.
        /// </summary>
        public RulerOperation()
        {
            _startPosition = null;
            _lastLengthString = null;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update(ToolOperationArgs args)
        {
            _currentPosition = args.MousePoint.ConvertToVector2();

            if (_startPosition == null)
            {
                // Start point hasn't been selected yet.
                if (args.IsLeftMouseButtonClicked())
                {
                    _startPosition = args.MousePoint.ConvertToVector2();
                }
            }
            else
            {
                // The user has selected a start position. Only reset the state when the mouseclick is released.
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
                Vector2 rulerDispacement = _currentPosition - _startPosition.Value;
                float worldDistance = rulerDispacement.Length();

                // Create a vector perpendicular to the ruler to draw tick marks.
                float tempX = rulerDispacement.X;
                Vector2 perpendicular = new Vector2(rulerDispacement.Y, -tempX);
                perpendicular.Normalize();

                // Draw the ruler line from the start to end position.
                args.SpriteBatch.DrawLine(_startPosition.Value, _currentPosition, Color.Blue);

                int numMarks = (int)(worldDistance / kDistanceBetweenTickMarks);
                numMarks = numMarks > 0 ? numMarks : 1;

                // The displacement between each tick mark.
                Vector2 tickOffset = rulerDispacement / (float)numMarks;

                // Draw tick marks along the line.
                rulerDispacement.Normalize();
                Vector2 tickStart = _startPosition.Value;
                for (int markIndex = 0; markIndex <= numMarks; ++markIndex)
                {
                    // Alternate between big and small tick marks.
                    float currentTickLength = kTickLength;
                    if (markIndex != 0 && markIndex != numMarks)
                    {
                        if (markIndex % 2 == 1)
                        {
                            // Small tick marks are half the size of the big ones.
                            currentTickLength = kTickLength / 2.0f;
                        }
                    }
                    
                    Vector2 tickEnd = tickStart + (perpendicular * currentTickLength);
                    args.SpriteBatch.DrawLine(tickStart, tickEnd, Color.Blue);
                    tickStart += tickOffset;
                }

                // Draw a string with the length that the ruler is measuring.
                _lastLengthString = string.Format("Length: {0}", worldDistance);
                args.SpriteBatch.DrawString(args.SpriteFont, _lastLengthString, _currentPosition, Microsoft.Xna.Framework.Color.Black, 0.0f, Vector2.Zero, kLabelScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1.0f);
            }
            else
            {
                _lastLengthString = null;
            }
        }
    }
}
