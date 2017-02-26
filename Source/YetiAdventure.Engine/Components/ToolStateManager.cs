using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using YetiAdventure.Shared.Common;
using YetiAdventure.Engine.Interfaces;
using Microsoft.Xna.Framework;
using YetiAdventure.Engine.Common;
using Microsoft.Xna.Framework.Graphics;
using Prism.Events;
using YetiAdventure.Engine.Components.BuilderOperations;

namespace YetiAdventure.Engine.Components
{

    //todo: implment operations
    //hook up selected primitive
    //hook up active tool
    //hook up tool state manager
    //hook up level builder service


    /// <summary>
    /// manages level builder state
    /// </summary>
    public class ToolStateManager : IToolStateManager
    {
        private readonly Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>> _toolUpdateOperations;
        private readonly Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>> _toolDrawOperations;

        MouseState _lastMouseState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStateManager"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ToolStateManager(IEventAggregator eventAggregator)
        {
            _polygonOperator = new PolygonOperation(eventAggregator);
            _toolUpdateOperations = new Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>>()
            {
                { LevelBuilderTool.DrawPolygon, PerformPolygonUpdateOperation }
            };
            _toolDrawOperations = new Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>>()
            {
                { LevelBuilderTool.DrawPolygon, PerformPolygonDrawOperation }
            };
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStateManager"/> class.
        /// </summary>
        /// <param name="activeTool">The active tool.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ToolStateManager(LevelBuilderTool activeTool, IEventAggregator eventAggregator) : this(eventAggregator)
        {
            ActiveTool = activeTool;
        }
        
        /// <summary>
        /// Gets or sets the active tool.
        /// </summary>
        /// <value>
        /// The active tool.
        /// </value>
        public LevelBuilderTool ActiveTool { get; private set; }

        /// <summary>
        /// Sets the active tool.
        /// </summary>
        /// <param name="tool">The tool.</param>
        /// <returns></returns>
        public void SetActiveTool(LevelBuilderTool tool)
        {
            ActiveTool = tool;
        }


        /// <summary>
        /// Updates the tool state manager
        /// </summary>
        /// <param name="mousePoint">The mouse point.</param>
        /// <param name="mouseState">State of the mouse.</param>
        public void Update(Vector2 mousePoint, MouseState mouseState)
        {
            var action = GetUpdateOperation(ActiveTool);
            var args = GetUpdateOperationArguments(mousePoint, mouseState);
            var result = action(args);



            _lastMouseState = mouseState;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var updateAction = GetDrawOperation(ActiveTool);
            var args = GetDrawOperationArguments(spriteBatch, gameTime);
            var result = updateAction(args);


        }
        #region operators

        PolygonOperation _polygonOperator;

        /// <summary>
        /// Performs the polygon update operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private ToolOperationResult PerformPolygonUpdateOperation(ToolOperationArgs args)
        {
            _polygonOperator.Update(args);
            return new ToolOperationResult();

        }

        /// <summary>
        /// Performs the polygon draw operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private ToolOperationResult PerformPolygonDrawOperation(ToolOperationArgs args)
        {
            _polygonOperator.Draw(args);
            return new ToolOperationResult();
        }
        #endregion


        /// <summary>
        /// Gets the update operation.
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <param name="tool">The tool.</param>
        /// <returns></returns>
        internal ToolOperationAction<ToolOperationResult, ToolOperationArgs> GetUpdateOperation(LevelBuilderTool tool)
        {
            if (_toolUpdateOperations.ContainsKey(tool)) return _toolUpdateOperations[tool];

            return (arg => { return new ToolOperationResult(); });
        }

        /// <summary>
        /// Gets the update operation arguments.
        /// </summary>
        /// <param name="mousePoint">The mouse point.</param>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns></returns>
        internal ToolOperationArgs GetUpdateOperationArguments(Vector2 mousePoint, MouseState mouseState)
        {
            var args = new ToolOperationArgs()
            {
                PreviousMouseState = _lastMouseState,
                MousePoint = mousePoint,
                MouseState = mouseState,
                
            };
            return args;
        }

        /// <summary>
        /// Gets the draw operation.
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <param name="tool">The tool.</param>
        /// <returns></returns>
        internal ToolOperationAction<ToolOperationResult, ToolOperationArgs> GetDrawOperation(LevelBuilderTool tool)
        {
            if (_toolDrawOperations.ContainsKey(tool)) return _toolDrawOperations[tool];

            return (arg => { return new ToolOperationResult(); });
        }

        /// <summary>
        /// Gets the draw operation arguments.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        internal ToolOperationArgs GetDrawOperationArguments(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var args = new ToolOperationArgs()
            {
                PreviousMouseState = _lastMouseState,
                SpriteBatch = spriteBatch,
                GameTime = gameTime,

            };
            return args;
        }
    }
}
