using Microsoft.Xna.Framework;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Engine;

namespace YetiAdventure.Desktop
{
    public class YetiGame : Game
    {

        private YetiEngine _engine;
        private GraphicsDeviceManager _graphics;

        public YetiGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            
        }

        protected override void Initialize()
        {
            _engine = new YetiEngine(_graphics);
            _engine.Initialize();
            IsMouseVisible = _engine.IsMouseVisible;
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _engine.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
