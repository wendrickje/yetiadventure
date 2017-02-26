using Microsoft.Xna.Framework;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Engine;
using Prism.Events;
using Microsoft.Practices.Unity;

namespace YetiAdventure.Desktop
{
    public class YetiGame : Game
    {

        private YetiEngine _engine;
        private GraphicsDeviceManager _graphics;
        private IUnityContainer _container;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="YetiGame"/> class.
        /// </summary>
        public YetiGame(IUnityContainer container)
        {
            _graphics = new GraphicsDeviceManager(this);
            _container = container;
        }

        protected override void Initialize()
        {
            var eventAggregator = _container.Resolve<IEventAggregator>();
            _engine = new YetiEngine(_graphics, eventAggregator);
            _engine.RootContentPath = "Content";
            _engine.Initialize();
            IsMouseVisible = _engine.IsMouseVisible;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _engine.Update(gameTime);
            MouseState mouseState = Mouse.GetState();
            _engine.UpdateMousePosition(mouseState.X, mouseState.Y);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
