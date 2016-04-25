using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Common;
using YetiAdventure.Components;
using YetiAdventure.Interfaces;
using YetiAdventure.Levels;

namespace YetiAdventure
{
    public class YetiGameBootstrapper
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Level _currentLevel;
        GameController _gameController;
        GameAimer _gameAimer;
        
        public YetiGameBootstrapper(ContentManager content, GraphicsDeviceManager graphics, GameController controller, GameAimer aimer)
        {
            _graphics = graphics;
            Content = content;
            Content.RootDirectory = "Content";
            _gameController = controller;
            _gameAimer = aimer;

         }

        public ContentManager Content { get; set; }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here


            _currentLevel = LoadLevel();

            _currentLevel.Initalize();
        }

        Level LoadLevel()
        {
            return new DemoLevel();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(SpriteBatch spriteBatch)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = spriteBatch;



            _currentLevel.LoadContent(_spriteBatch, Content);
            _currentLevel.Player.Texture = Content.Load<Texture2D>("player");
            var start = _currentLevel.StartPosition;
            var playerPosition = new Vector2(start.X, start.Y - (Player.Height / 2));
            _currentLevel.Player.Position = playerPosition;
            _gameAimer.Position = playerPosition;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            _currentLevel.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            var player = _currentLevel.Player;

            _gameController.HandleInputOnPlayer(player);

            _currentLevel.Update(gameTime);
            _gameAimer.Update(gameTime);
            _gameAimer.ClampToBounds(player, player.Direction);
            _gameAimer.HandleInputOnPlayer(player);
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentLevel.Draw(gameTime, _spriteBatch);
            _gameAimer.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            
        }
    }
}
