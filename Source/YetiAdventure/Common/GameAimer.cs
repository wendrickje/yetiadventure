using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Common
{
    public abstract class GameAimer : IDrawableObject, IGameController
    {

        private const float AimingRadius = 100;
        private const float ReloadTime = 50;

        public GameAimer(Texture2D texture)
        {
            Texture = texture;
        }

        public abstract bool IsShooting();

        public Vector2 Target { get; set; }
        public abstract void Update(GameTime gameTime);

        private Vector2 _oldPosition;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);

            
        }

        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }

        public Rectangle Container { get; set; }

        public void ClampToBounds(IDrawableObject drawable, Direction direction)
        {
            var bindingVector = drawable.Position;
            
            
            //get top left of binding vector
            var center = new Vector2(bindingVector.X - (Texture.Width / 2), bindingVector.Y - (Texture.Height / 2));
            //add half width and height of binding vectors texture for the actual center
            center.X = center.X + drawable.Container.Width / 2;
            center.Y = center.Y + drawable.Container.Height / 2;

            //clamp position

            


            // Calculate the offset vector from the center of the circle to our position
            Vector2 offset = Target - center;
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
