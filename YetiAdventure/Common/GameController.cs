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


    public abstract class GameController<T> : IGameController
    {

        public GameController(GameControllerType controllerType)
        {
            ControllerType = controllerType;
        }

        public GameControllerType ControllerType { get; private set; }

        public void HandleInputOnPlayer(Player player)
        {
            if (IsButtonDown(ButtonReset))
                player.Position = new Vector2();

            if (!player.IsJumping || !IsButtonDown(ButtonRight) || !IsButtonDown(ButtonLeft))
            {
                player.Velocity = new Vector2();
            }

            if (IsButtonDown(ButtonRight))
            {
                player.Velocity = new Vector2(Player.WalkSpeed, player.Velocity.Y);
                player.Direction = Direction.East;
            }
            if (IsButtonDown(ButtonLeft))
            {
                player.Velocity = new Vector2(Player.WalkSpeed * -1, player.Velocity.Y);
                player.Direction = Direction.West;
            }

            //jump on push, but you cant jump again until you have released jump
            if (IsButtonDown(ButtonJump) && player.CanJump() && isjumpreleased)
            {
                isjumppressed = true;
                player.IsJumping = true;
                isjumpreleased = false;
            }

            if (isjumppressed && IsButtonUp(ButtonJump) && player.CanJump())
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

        protected void Aim()
        {

        }

        public abstract T ButtonReset { get; }

        public abstract bool IsButtonUp(T button);

        public abstract bool IsButtonDown(T button);

        public abstract T ButtonRight { get; }

        public abstract T ButtonLeft { get; }

        public abstract T ButtonUp { get; }

        public abstract T ButtonDown { get; }

        public abstract T ButtonJump { get; }
        

    }

}
