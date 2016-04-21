using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YetiAdventure.Common;
using YetiAdventure.Components;
using YetiAdventure.Interfaces;
using YetiAdventure.Levels;

namespace YetiAdventure
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class YetiGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Level _currentLevel;
        IGameController _gameController;
        GameAimer _gameAimer;

        public YetiGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _gameController = new KeyboardController();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            _currentLevel = LoadLevel();
            
            _currentLevel.Initalize();
            base.Initialize();
        }

        Level LoadLevel()
        {
            return new DemoLevel();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var pointTexture = Content.Load<Texture2D>("mousepointer");
            _gameAimer = new MouseAimer(pointTexture);


            _currentLevel.LoadContent(_spriteBatch, Content);
            _currentLevel.Player.Texture = Content.Load<Texture2D>("player");
            var start = _currentLevel.StartPosition;
            _currentLevel.Player.Position = new Vector2(start.X, start.Y - (Player.Height / 2));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            _currentLevel.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //GamePad.GetState(PlayerIndex.One).Buttons
            _gameController.HandleInputOnPlayer(_currentLevel.Player);

            _currentLevel.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            _currentLevel.Draw(gameTime, _spriteBatch);
            _spriteBatch.Draw(_gameAimer.Texture, _gameAimer.Position, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
