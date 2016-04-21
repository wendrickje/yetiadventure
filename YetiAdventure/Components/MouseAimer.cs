
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
        }

        public override bool IsShooting()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public override Vector2 Position
        {
            get
            {
                var mouse = Mouse.GetState();
                return new Vector2((mouse.X - (Texture.Width / 2)), (mouse.Y - (Texture.Height / 2)));
            }
        }
    }
}
