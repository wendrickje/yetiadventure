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
            if (IsButtonDown(GameButton.Jump) && player.CanJump() && _isjumpreleased)
            {
                _isjumppressed = true;
                player.IsJumping = true;
                _isjumpreleased = false;
            }

            if (_isjumppressed && IsButtonUp(GameButton.Jump) && player.CanJump())
            {
                _isjumpreleased = true;
                _isjumppressed = false;
            }
            if (_isjumpreleased && _isjumppressed)
            {
                _isjumppressed = false;
                _isjumpreleased = false;
            }
        }

        bool _isjumppressed = false;
        bool _isjumpreleased = true;

    
        

        public abstract bool IsButtonUp(GameButton button);

        public abstract bool IsButtonDown(GameButton button);
        

    }

}
