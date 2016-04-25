using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Common;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Common
{


    public abstract class GameController : IGameController
    {

        public GameController(GameControllerType controllerType)
        {
            ControllerType = controllerType;
        }

        public GameControllerType ControllerType { get; private set; }

        public void HandleInputOnPlayer(Player player)
        {
            if (IsButtonDown(GameButton.Reset))
                player.Position = new Vector2();

            if (!player.IsJumping || !IsButtonDown(GameButton.Right) || !IsButtonDown(GameButton.Left))
            {
                player.Velocity = new Vector2();
            }

            if (IsButtonDown(GameButton.Right))
            {
                player.Velocity = new Vector2(Player.WalkSpeed, player.Velocity.Y);
                player.Direction = Direction.East;
            }
            if (IsButtonDown(GameButton.Left))
            {
                player.Velocity = new Vector2(Player.WalkSpeed * -1, player.Velocity.Y);
                player.Direction = Direction.West;
            }

            //jump on push, but you cant jump again until you have released jump
            if (IsButtonDown(GameButton.Jump) && player.CanJump() && isjumpreleased)
            {
                isjumppressed = true;
                player.IsJumping = true;
                isjumpreleased = false;
            }

            if (isjumppressed && IsButtonUp(GameButton.Jump) && player.CanJump())
            {
                isjumpreleased = true;
                isjumppressed = false;
            }
            if (isjumpreleased && isjumppressed)
            {
                isjumppressed = false;
                isjumpreleased = false;
            }
        }

        bool isjumppressed = false;
        bool isjumpreleased = true;

    

        //public abstract GameButton ButtonReset { get; }

        public abstract bool IsButtonUp(GameButton button);

        public abstract bool IsButtonDown(GameButton button);

        //public abstract GameButton ButtonRight { get; }

        //public abstract GameButton ButtonLeft { get; }

        //public abstract GameButton ButtonUp { get; }

        //public abstract GameButton ButtonDown { get; }

        //public abstract GameButton ButtonJump { get; }
        

    }

}
