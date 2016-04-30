using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Common;

namespace YetiAdventure.Components
{

    public class KeyboardController : GameController
    {

        public KeyboardController()
            : base(GameControllerType.Keyboard)
        {

        }

        Dictionary<GameButton, Keys> ButtonToKeyboardMapper = new Dictionary<GameButton, Keys>
        {
            { GameButton.Reset, Keys.Enter},
            { GameButton.Jump, Keys.Space},
            { GameButton.Right, Keys.D},
            { GameButton.Left, Keys.A},
            { GameButton.Up, Keys.W},
            { GameButton.Down, Keys.S},
        };


        public override bool IsButtonDown(GameButton button)
        {
            var key = ButtonToKeyboardMapper[button];
            return Keyboard.GetState().IsKeyDown(key);
        }

        public override bool IsButtonUp(GameButton button)
        {
            var key = ButtonToKeyboardMapper[button];
            return Keyboard.GetState().IsKeyUp(key);
        }



    }
}
