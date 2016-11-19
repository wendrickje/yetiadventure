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

namespace YetiAdventure.LevelBuilder.Controls
{
    public class LevelCanvasControl : GameControl
    {
        private YetiEngine _engine;
        protected override void Initialize()
        {
            _engine = new YetiEngine(GraphicsDeviceService);
            // Need to tell the engine that it's in editor mode
            _engine.IsInEditor = true;

            // Provide the desktop content content path.
            string executableRoot = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            _engine.RootContentPath = string.Format("{0}{1}", executableRoot, "/../../../YetiAdventure.Desktop/Content/bin/DesktopGL/");

            _engine.Initialize();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {

            _engine.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

        }

        protected override void SetEditorMousePosition(int inX, int inY)
        {
            _engine.UpdateMousePosition(inX, inY);
        }
    }
}
