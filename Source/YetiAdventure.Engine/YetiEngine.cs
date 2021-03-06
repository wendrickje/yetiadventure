﻿using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PhysicsLogic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Levels;
using YetiAdventure.Engine.Physics;
using YetiAdventure.Shared.Interfaces;
using YetiAdventure.Engine.Interfaces;
using YetiAdventure.Shared.Models;
using YetiAdventure.Engine.Components;
using Prism.Events;
using YetiAdventure.Shared.Common;
using Color = Microsoft.Xna.Framework.Color;
using System.Threading.Tasks;

namespace YetiAdventure.Engine
{

    /// <summary>
    /// The main class for the engine, handles all of the manager and service updates.
    /// </summary>
    public class YetiEngine : IEngineProvider, ILevelBuilderService
    {
        private static YetiEngine _sInstance;

        private GameServiceContainer _services;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        //AnimatedSprite _eddySprites;
        Camera _camera;

        EditMode _editMode;
        Vector2 _mousePosition;

        // Camera panning with middle mouse button
        bool _bPanningCamera;
        Vector2 _panStartPosition;
        Vector2 _panCurrentDisplacement;
        Vector2 _previousMousePosition;

        Random _random;
        MouseState _lastMouseState;

        Level _mainLevel;

        Body _fallingBody = null;
        Body _groundBody = null;

        List<Body> _boxBodies = new List<Body>();

        SimpleExplosion _explosion;
        PhysicalVortex _vortex;

        List<Vector2> _polygonVertices;
        List<Vector2> _navCollisions = new List<Vector2>();
        Dictionary<int, Primitive> _primitives;

        IToolStateManager _toolStateManager;
        IPrimitiveManager _primitiveManager;
        IEventAggregator _eventAggregator;
        public bool IsMouseVisible { get; set; }

        public string RootContentPath { get; set; }

        /// <summary>
        /// Switch used to determine which mouse input method to use, because Standalone vs. Embedded in the editor mouse behaves differently.
        /// </summary>
        public bool IsInEditor { get; set; }

        public Camera Camera { get { return _camera; } }
        
        /// <summary>
        /// Retrieve the static instance of the YetiEngine.
        /// </summary>
        public static YetiEngine Instance { get { return _sInstance; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="YetiEngine"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public YetiEngine(IGraphicsDeviceService graphics, IEventAggregator eventAggregator)
        {
            _sInstance = this;

            _eventAggregator = eventAggregator;
            _services = new GameServiceContainer();
            _services.AddService(typeof(IGraphicsDeviceService), graphics);

            _graphicsDevice = graphics.GraphicsDevice;

            _content = new ContentManager(_services);
            _content.RootDirectory = "Content";
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _mousePosition = new Vector2();

            _polygonVertices = new List<Vector2>();
            _primitives = new Dictionary<int, Primitive>();

            _editMode = EditMode.CreatePolygon;
            

            IsMouseVisible = true;

            _mainLevel = new Level();
        }

        private void CreateGroundAndPlayer(World physicalWorld)
        {
            _groundBody = BodyFactory.CreateRectangle(physicalWorld, 100, 10.0f, 1.0f);
            _groundBody.Friction = 10.0f;
            _groundBody.SetTransform(new Vector2(0.0f, 0.0f), 0.0f);

            // Circle with meter diameter
            _fallingBody = BodyFactory.CreateCircle(physicalWorld, 0.5f, 1.0f);
            _fallingBody.AngularDamping = 20f;
            _fallingBody.Friction = 10.0f;
            _fallingBody.SetTransform(new Vector2(0.0f, -20.0f), 0.0f);
            _fallingBody.IsStatic = false;

        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            _content.RootDirectory = RootContentPath;
            TextureManager.Initialize(_content);

            _camera = new Camera(_graphicsDevice);
            _camera.Zoom = 20.0f;
            _camera.TargetPosition = Vector2.Zero;

            _spriteFont = _content.Load<SpriteFont>("Arial");

            _primitiveManager = new PrimitiveManager(_eventAggregator);
            _toolStateManager = new ToolStateManager(_eventAggregator, _primitiveManager);

            World physicalWorld = PhysicsEngine.GetSingleton().PhysicsWorld;

            CreateGroundAndPlayer(physicalWorld);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleEditorPanning(ref MouseState mouseState)
        {
            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                if (_bPanningCamera == false)
                {
                    _bPanningCamera = true;
                    _panStartPosition = _previousMousePosition = _mousePosition;
                }
                else if (_bPanningCamera == true)
                {
                    // Compute the camera displacement based on screenspace mouse position.
                    Vector2 worldSpaceStartPanPos = _camera.ConvertScreenToWorld(_previousMousePosition);
                    Vector2 worldSpaceCurrentPanPos = _camera.ConvertScreenToWorld(_mousePosition);
                    Vector2 worldspacePanDisplacement = (_previousMousePosition - _mousePosition) / _camera.Zoom;
                    _camera.TargetPosition += worldspacePanDisplacement;
                }

            }
            else if (mouseState.MiddleButton == ButtonState.Released)
            {
                _bPanningCamera = false;
            }
        }


        /// <summary>
        /// Updates the engine using specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            var kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            
            if (kbState.IsKeyDown(Key.Right))
            {
                if (_fallingBody.AngularVelocity > 0.0f)
                {
                    _fallingBody.AngularVelocity = 0.0f;
                }
                _fallingBody.ApplyTorque(200.0f);
            }
            if (kbState.IsKeyDown(Key.Left))
            {
                if (_fallingBody.AngularVelocity < 0.0f)
                {
                    _fallingBody.AngularVelocity = 0.0f;
                }
                _fallingBody.ApplyTorque(-200.0f);
            }

            if (kbState.IsKeyDown(Key.Space))
            {
                _fallingBody.ApplyLinearImpulse(Vector2.UnitY * -1.0f);
            }

            if (kbState.IsKeyDown(Key.C))
            {
                _editMode = EditMode.CreatePolygon;
            }
            if (kbState.IsKeyDown(Key.J))
            {
                _editMode = EditMode.CreateJunk;
            }

            World physicsWorld = PhysicsEngine.GetSingleton().PhysicsWorld;

            int probeCount = 500;
            Vector2 startPoint = new Vector2(-43.5f, -30.0f);
            Vector2 endPoint = new Vector2(43.5f, -30.0f);

            Vector2 increment = Vector2.UnitX * ((endPoint - startPoint) / probeCount);

            Vector2 sweepLine = endPoint - startPoint;

            _navCollisions.Clear();
            for (int probeIndex = 0; probeIndex < probeCount; probeIndex++)
            {
                Vector2 currentStartPoint = startPoint + (increment * probeIndex);
                Vector2 currentEndPoint = currentStartPoint;
                currentEndPoint.Y = 0.0f;

                physicsWorld.RayCast(
                    (f, start, end, fr) =>
                    {
                        _navCollisions.Add(start);
                        return 1;
                    }, currentStartPoint, currentEndPoint);
            }

            _camera.Update(gameTime);
            MouseState mouseState = Mouse.GetCursorState();
            var mouseWorldPosition = _camera.ConvertScreenToWorld(_mousePosition);

            _toolStateManager.Update(mouseWorldPosition, mouseState);

            if (_editMode == EditMode.CreateJunk)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    SpawnBoxAtLocation(mouseWorldPosition);
                }
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    TriggerExplosion(mouseWorldPosition);
                }
                if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    TriggerVortex(mouseWorldPosition);
                }
            }
            HandleEditorPanning(ref mouseState);

            float zoomVariance = 0.25f;

            if (mouseState.ScrollWheelValue > _lastMouseState.ScrollWheelValue)
            {
                _camera.Zoom = _camera.Zoom * (1.0f + zoomVariance);
            }
            if (mouseState.ScrollWheelValue < _lastMouseState.ScrollWheelValue)
            {
                _camera.Zoom = _camera.Zoom * (1.0f - zoomVariance);
            }

            PhysicsEngine.GetSingleton().Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            _lastMouseState = mouseState;
            _previousMousePosition = _mousePosition;
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Microsoft.Xna.Framework.Color.CadetBlue);
            DrawLevel(gameTime);
            DrawInterface();
        }

        private void DrawLevel(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, _camera.Transform);

            _toolStateManager.Draw(_spriteBatch, gameTime, _spriteFont);
            _primitiveManager.Draw(_spriteBatch, gameTime, _spriteFont);



            PhysicsEngine.GetSingleton().Draw(_spriteBatch);

            for (int collisionIndex = 0; collisionIndex < _navCollisions.Count; collisionIndex++)
            {
                _spriteBatch.DrawCircle(_navCollisions[collisionIndex], 0.2f, 10, Color.Purple);
            }
            _spriteBatch.End();
        }

        public void DrawInterface()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (IsInEditor)
            {
                _spriteBatch.DrawString(_spriteFont, "Editor Mode", Vector2.One * 10.0f, Microsoft.Xna.Framework.Color.Black);
            }
            else
            {
                _spriteBatch.DrawString(_spriteFont, "Gameplay Mode", Vector2.One * 10.0f, Microsoft.Xna.Framework.Color.Black);
            }

            // Diplay the world position of the cursor within the editor control.
            Vector2 worldSpaceStartPanPos = _camera.ConvertScreenToWorld(_mousePosition);
            string worldPosString = string.Format("World Position: ({0}, {1})", worldSpaceStartPanPos.X, worldSpaceStartPanPos.Y);
            _spriteBatch.DrawString(_spriteFont, worldPosString, new Vector2(10.0f, 30.0f), Microsoft.Xna.Framework.Color.Black);
            _spriteBatch.End();
        }

        private void SpawnBoxAtLocation(Vector2 inLocation)
        {

            float bodyWidth = (float)(_random.NextDouble() * 1.0f) + 0.20f;
            float bodyHeight = (float)(_random.NextDouble() * 1.0f) + 0.20f;
            Body boxBody = BodyFactory.CreateRectangle(PhysicsEngine.GetSingleton().PhysicsWorld, bodyWidth, bodyHeight, 0.5f);
            boxBody.IsStatic = false;

            float randomAngle = (float)(_random.NextDouble() * MathHelper.TwoPi);

            boxBody.SetTransform(inLocation, randomAngle);
            _boxBodies.Add(boxBody);
        }

        private void TriggerExplosion(Vector2 inLocation)
        {
            if (_explosion == null)
            {
                _explosion = new SimpleExplosion(PhysicsEngine.GetSingleton().PhysicsWorld);
            }

            _explosion.Power = 1.0f;
            _explosion.Activate(inLocation, 5.0f, 5.0f);
        }

        private void TriggerVortex(Vector2 inPosition)
        {
            if (_vortex == null)
            {
                _vortex = new PhysicalVortex(PhysicsEngine.GetSingleton().PhysicsWorld);
            }

            _vortex.MaxForce = 100;
            _vortex.Activate(inPosition, 5.0f, 1.0f);
        }

        public void Exit()
        {
            //platform specific exit 
        }

        public void UpdateMousePosition(int inX, int inY)
        {
            _mousePosition.X = inX;
            _mousePosition.Y = inY;
        }


        #region level builder actions

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns></returns>
        public Shared.Common.Point GetMousePosition()
        {
            var mouseState = Mouse.GetCursorState();
            var point = new Shared.Common.Point(mouseState.X, mouseState.Y);
            return point;
        }

        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="position"></param>
        /// <param name="color">The color.</param>
        public void DrawString(string value, Shared.Common.Point position, Shared.Common.Color color)
        {

            _spriteBatch.Begin();
            var vector = position.ConvertToVector2();
            var cc = color.ConvertToColor();
            _spriteBatch.DrawString(_spriteFont, value, vector, cc);
            _spriteBatch.End();
        }

        /// <summary>
        /// Gets the primitive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Primitive GetPrimitive(Guid id)
        {
            return _primitiveManager.GetPrimitive(id);
        }

        public void SetActiveLevelBuilderTool(Shared.Common.LevelBuilderTool tool)
        {
            _toolStateManager.SetActiveTool(tool);
        }


        /// <summary>
        /// Moves the primitive.
        /// </summary>
        /// <param name="primitive">The primitive.</param>
        /// <param name="point">The point to move to.</param>
        public void MovePrimitive(Primitive primitive, Shared.Common.Point point)
        {
            _primitiveManager.MovePrimitive(primitive, point);
        }

        public async Task UpdatePrimitive(Guid id, Primitive source)
        {
            await _primitiveManager.UpdatePrimitive(id, source);

        }

        #endregion
    }
}
