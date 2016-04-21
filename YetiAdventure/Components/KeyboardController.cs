using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Common;

namespace YetiAdventure.Components
{

    public class KeyboardController : GameController<Keys>
    {

        public KeyboardController()
            : base(GameControllerType.Keyboard)
        {

        }

        public override Keys ButtonReset { get { return Keys.Enter; } }

        public override Keys ButtonJump { get { return Keys.Space; } }

        public override Keys ButtonRight { get { return Keys.D; } }

        public override Keys ButtonLeft { get { return Keys.A; } }

        public override Keys ButtonUp { get { return Keys.W; } }

        public override Keys ButtonDown { get { return Keys.S; } }


        public override bool IsButtonDown(Keys button)
        {
            return Keyboard.GetState().IsKeyDown(button);
        }

        public override bool IsButtonUp(Keys button)
        {
            return Keyboard.GetState().IsKeyUp(button);
        }



    }
}
