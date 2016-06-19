using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetiAdventure.Engine
{

    /// <summary>
    /// The main class for the engine, handles all of the manager and service updates.
    /// </summary>
    public class YetiEngine 
    {
        private GameServiceContainer _services;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;


        public YetiEngine(IGraphicsDeviceService graphics)
        {
            _services = new GameServiceContainer();
            _services.AddService(typeof(IGraphicsDeviceService), graphics);

            _graphicsDevice = graphics.GraphicsDevice;

            _content = new ContentManager(_services);

            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.CadetBlue);

            _spriteBatch.Begin();

            _spriteBatch.End();
        }


    }
}
