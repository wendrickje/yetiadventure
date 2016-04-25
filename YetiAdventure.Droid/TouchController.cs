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
using Android.Gestures;

namespace YetiAdventure.Droid
{
    public class TouchController : GameController
    {
        public TouchController() : base(GameControllerType.Touch)
        {
        }
        
        public override bool IsButtonDown(GameButton button)
        {
            return false;
        }

        public override bool IsButtonUp(GameButton button)
        {
            return false;
        }
    }
}