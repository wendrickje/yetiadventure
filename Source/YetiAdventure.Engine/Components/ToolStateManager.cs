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
    /// <summary>
    /// manages level builder state
    /// </summary>
    public class ToolStateManager : IToolStateManager
    {
        private readonly Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>> _toolUpdateOperations;
        private readonly Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>> _toolDrawOperations;

        MouseState _previousMouseState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStateManager"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="primitiveManager">The primitive manager.</param>
        /// <param name="activeTool">The active tool.</param>
        public ToolStateManager(IEventAggregator eventAggregator, IPrimitiveManager primitiveManager, LevelBuilderTool activeTool = LevelBuilderTool.Selector)
        {
            //todo: dictionary is fine for now but probably want a true factory pattern implementation

            _polygonOperator = new PolygonOperation(eventAggregator, primitiveManager);
            _selectionOperator = new SelectionOperation(eventAggregator, primitiveManager);
            _transformOperator = new TransformOperation(eventAggregator, primitiveManager);

            _toolUpdateOperations = new Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>>()
            {
                { LevelBuilderTool.DrawPolygon, PerformPolygonUpdateOperation },
                { LevelBuilderTool.Selector,  PerformSelectionUpdateOperation },
                { LevelBuilderTool.Transform,  PerformTransformUpdateOperation }
            };
            _toolDrawOperations = new Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>>()
            {
                { LevelBuilderTool.DrawPolygon, PerformPolygonDrawOperation },
                { LevelBuilderTool.Selector,  PerformSelectionDrawOperation },
                { LevelBuilderTool.Transform,  PerformTransformDrawOperation }
            };
        }

        #region operators

        private PolygonOperation _polygonOperator;
        private SelectionOperation _selectionOperator;
        private TransformOperation _transformOperator;

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

        /// <summary>
        /// Performs the selection update operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private ToolOperationResult PerformSelectionUpdateOperation(ToolOperationArgs args)
        {
            _selectionOperator.Update(args);
            return new ToolOperationResult();
        }

        /// <summary>
        /// Performs the selection draw operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private ToolOperationResult PerformSelectionDrawOperation(ToolOperationArgs args)
        {
            _selectionOperator.Draw(args);
            return new ToolOperationResult();
        }

        /// <summary>
        /// Performs the transform draw operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private ToolOperationResult PerformTransformDrawOperation(ToolOperationArgs args)
        {
            _transformOperator.Draw(args);
            return new ToolOperationResult();
        }

        /// <summary>
        /// Performs the transform update operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private ToolOperationResult PerformTransformUpdateOperation(ToolOperationArgs args)
        {
            _transformOperator.Update(args);
            return new ToolOperationResult();

        }
        #endregion


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
            var point = mousePoint.ConvertToSharedPoint();
            var action = GetOperation(ActiveTool, _toolUpdateOperations);
            var args = GetUpdateOperationArguments(point, mouseState, _previousMouseState);
            var result = action(args);


            //update prev state
            _previousMouseState = mouseState;

        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="font">The font.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            var updateAction = GetOperation(ActiveTool, _toolDrawOperations);
            var args = GetDrawOperationArguments(spriteBatch, gameTime, font);
            var result = updateAction(args);



        }
        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <param name="tool">The tool.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        internal ToolOperationAction<ToolOperationResult, ToolOperationArgs> GetOperation(LevelBuilderTool tool, Dictionary<LevelBuilderTool, ToolOperationAction<ToolOperationResult, ToolOperationArgs>> source)
        {
            ToolOperationAction<ToolOperationResult, ToolOperationArgs> operation;
            if (source.TryGetValue(tool, out operation))
                return operation;

            return (arg => { return new ToolOperationResult(); });
        }

        /// <summary>
        /// Gets the update operation arguments.
        /// </summary>
        /// <param name="mousePoint">The mouse point.</param>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <returns></returns>
        internal ToolOperationArgs GetUpdateOperationArguments(Shared.Common.Point mousePoint, MouseState mouseState, MouseState previousMouseState)
        {
            var args = new ToolOperationArgs()
            {
                PreviousMouseState = previousMouseState,
                MousePoint = mousePoint,
                MouseState = mouseState,
                
            };
            return args;
        }


        /// <summary>
        /// Gets the draw operation arguments.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        internal ToolOperationArgs GetDrawOperationArguments(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            var args = new ToolOperationArgs()
            {
                PreviousMouseState = _previousMouseState,
                SpriteBatch = spriteBatch,
                GameTime = gameTime,
                SpriteFont = font,
            };
            return args;
        }
    }
}
