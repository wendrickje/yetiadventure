using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using YetiAdventure.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace YetiAdventure.Droid
{
    public class TouchAimer : GameAimer
    {
        public TouchAimer(Texture2D texture) : base(texture)
        {
        }

        public override Vector2 Position
        {
            get;

            set;
        }

        public override bool IsShooting()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}