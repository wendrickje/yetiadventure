
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YetiAdventure.Components
{
    public class MouseAimer : GameAimer
    {
        public MouseAimer(Texture2D texture) :base(texture)
        {
            Target = new Vector2();
        }

        bool _isMouseDown;
        bool _isMouseUp;
        bool _hasClicked;
        bool _isClicking;

        public override bool IsShooting()
        {
            if (!_isClicking)
            {
                _isMouseDown = Mouse.GetState().LeftButton == ButtonState.Pressed;
            }

            if(_isMouseDown)
            {
                _isClicking = true;
            }

            if(_isClicking)
            {
                _isMouseUp = Mouse.GetState().LeftButton == ButtonState.Released;

            }

            if(_isMouseUp)
            {
                _hasClicked = true;
            }


            if (_hasClicked)
            {
                //reset
                _isClicking = false;
                _isMouseUp = false;
                _isMouseDown = false;
                _hasClicked = false;

                return true;
            }
            else
            {
                return false;
            }
            

            
        }

        public override void Update(GameTime gameTime)
        {

            var mouse = Mouse.GetState();
            Target = new Vector2(mouse.X - (Texture.Width / 2), mouse.Y - (Texture.Height / 2));
        }

        
    }
}
