using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using YetiAdventure.Shared.Common;

namespace YetiAdventure.Engine.Common
{
    /// <summary>
    /// tool operation base
    /// </summary>
    public class ToolOperationBase
    {

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void Update(ToolOperationArgs args)
        {

        }
        /// <summary>
        /// Draws the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public virtual void Draw(ToolOperationArgs args)
        {

        }
    }

    /// <summary>
    /// tool operation args
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ToolOperationArgs : EventArgs
    {
        public GameTime GameTime { get; internal set; }

        /// <summary>
        /// Gets the mouse point.
        /// </summary>
        /// <value>
        /// The mouse point.
        /// </value>
        public Shared.Common.Point MousePoint { get; set; }

        /// <summary>
        /// Gets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        public MouseState MouseState { get; set; }

        /// <summary>
        /// Gets or sets the state of the previous mouse.
        /// </summary>
        /// <value>
        /// The state of the previous mouse.
        /// </value>
        public MouseState PreviousMouseState { get; set; }

        /// <summary>
        /// Gets or sets the sprite batch.
        /// </summary>
        /// <value>
        /// The sprite batch.
        /// </value>
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Gets the sprite font.
        /// </summary>
        /// <value>
        /// The sprite font.
        /// </value>
        public SpriteFont SpriteFont { get; set; }
    }

    /// <summary>
    /// tool operation results
    /// </summary>
    public class ToolOperationResult
    {

    }

    /// <summary>
    /// tool operation delegate
    /// </summary>
    /// <param name="args">The arguments.</param>
    public delegate T ToolOperationAction<T, T1>(T1 args) where T : ToolOperationResult where T1 : ToolOperationArgs;


    /// <summary>
    /// Tool Operation Extensions
    /// </summary>
    public static class ToolOperationExtensions
    {
        /// <summary>
        /// Verifies the left mouse click action was performed.
        /// </summary>
        /// <returns></returns>
        public static bool IsLeftMouseButtonClicked(this ToolOperationArgs args)
        {
            var mouseState = args.MouseState;
            var prevousMouseState = args.PreviousMouseState;
            return prevousMouseState != null && mouseState != null && (mouseState.LeftButton == ButtonState.Pressed && prevousMouseState.LeftButton == ButtonState.Released);
        }
    }
}
