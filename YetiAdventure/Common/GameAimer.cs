using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Common
{
    public abstract class GameAimer : IDrawableObject, IGameController
    {

        private const float AimingRadius = 50;
        private const float ReloadTime = 5;

        public GameAimer(Texture2D texture)
        {
            Texture = texture;
        }

        public abstract bool IsShooting();

        public abstract void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public abstract Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }

        public Rectangle Container { get; set; }

        public void ClampToBounds(IDrawableObject drawable, Direction direction)
        {
            var directionAdjustment = direction == Direction.West ? -1 : 1;
            //get center point of bounds
            var bindingVector = drawable.Position;
            var center = new Vector2(bindingVector.X / 2, bindingVector.Y / 2);

            var currentPosistion = Position;
            
            // Calculate the offset vector from the center of the circle to our position
            Vector2 offset = currentPosistion - center;
            // Calculate the linear distance of this offset vector
            float distance = offset.Length();
            if (distance != AimingRadius)
            {
                // If the distance is more than our radius we need to clamp
                // Calculate the direction to our position
                Vector2 dir = offset / distance;
                // Calculate our new position using the direction to our old position and our radius
                Position = center + dir * AimingRadius;
            }
            
            

        }

         

        bool _isReloading;
        int _reloadTicker;
        public void HandleInputOnPlayer(Player player)
        {
            
            if(IsShooting() && !_isReloading)
            {
                _isReloading = true;
                player.Shoot(Position);
            }

            if(_isReloading)
            {
                _reloadTicker++;
            }

            if (_reloadTicker == ReloadTime)
            {
                _reloadTicker = 0;
                _isReloading = false;
            }
        }
    }
}
