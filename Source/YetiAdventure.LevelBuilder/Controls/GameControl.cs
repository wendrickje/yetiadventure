using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using YetiAdventure.LevelBuilder.Common;
using System.Windows.Forms;

namespace YetiAdventure.LevelBuilder.Controls
{
    public abstract class GameControl : GraphicsDeviceControl
    {
        DispatcherTimer _timer;
        Stopwatch _stopWatch;
        TimeSpan _totalTime = new TimeSpan();
        GameTime _gameTime;

        protected override void Initialize()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Background);

            _timer.Tick += (s, a) =>
            {
                var elapsed = _stopWatch.Elapsed;
                _totalTime = _totalTime + elapsed;
                _gameTime = new GameTime(_totalTime, elapsed);
                GameLoop();
            };
            _stopWatch = Stopwatch.StartNew();
            _timer.Start();
        }

        protected override void Draw()
        {
            Draw(_gameTime);
        }

        private void GameLoop()
        {
            Update(_gameTime);
            Invalidate();
        }

        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);

        

    }
}
