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
using System.Threading;

namespace YetiAdventure.LevelBuilder.Controls
{
    public abstract class GameControl : GraphicsDeviceControl
    {
        DispatcherTimer _timer;
        Stopwatch _stopWatch;
        GameTime _gameTime;

        public GameControl() : base()
        {

        }

        protected override void Initialize()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Background);

            var lastTick = new TimeSpan();
            _timer.Tick += (s, a) =>
            {

                if (!Focused)
                {
                    _stopWatch.Stop();
                    return;
                }
                if (!_stopWatch.IsRunning)
                {
                    _stopWatch.Start();
                }
                var totalTime = _stopWatch.Elapsed;
                var elapsed = totalTime - lastTick;
                lastTick = totalTime;

                _gameTime = new GameTime(totalTime, elapsed);
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
