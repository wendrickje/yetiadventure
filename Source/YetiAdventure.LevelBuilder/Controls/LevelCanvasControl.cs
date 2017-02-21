using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.LevelBuilder.Common;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Windows.Forms;
using YetiAdventure.Engine;
using OpenTK.Input;
using Microsoft.Practices.Unity;
using YetiAdventure.Shared.Interfaces;
using YetiAdventure.Engine.Levels;

namespace YetiAdventure.LevelBuilder.Controls
{
    public class LevelCanvasControl : GameControl
    {
        private YetiEngine _engine;
        private IUnityContainer _container;

        public LevelCanvasControl(IUnityContainer container) : base()
        {
            _container = container;
        }

        protected override void Initialize()
        {

            base.Initialize();
            _engine = new YetiEngine(GraphicsDeviceService);
            _engine.Initialize();
            _container.RegisterInstance<ILevelBuilderService>(_engine);
            _engine.LoadContent();


        }

        protected override void Update(GameTime gameTime)
        {

            _engine.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            
        }



    }
}
