using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace YetiAdventure.Common
{
    public abstract class Projectile : CollidableObject
    {
        public abstract float Weight { get; }

        public Projectile(DrawableObjectType ot) : base(ot)
        {
        }

        public bool IsDead { get; set; }
        float _time;
        public override void Update(GameTime gameTime)
        {

            //move the projectile along until 
            //when it reaches its apex kill it

            var initialPosition = Position;

            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position = initialPosition + Velocity * _time
                       + 0.5f * _acceleration * _time * _time;

           

        }

        /// <summary>
        /// starting point of the projectile
        /// </summary>
        public Vector2 Origin { get; private set; }

        /// <summary>
        /// direction in which the projectile is moving
        /// </summary>
        public Direction Direction { get; private set; }

        /// <summary>
        /// Target of projectile
        /// </summary>
        public Vector2 Target { get; private set; }


        private float _angle;

        private Vector2 _acceleration;


        public void Initalize(Vector2 origin, Vector2 target, float force)
        {
            Origin = origin;
            Target = target;
            var acc = force / Weight;
            _acceleration = new Vector2(acc, acc * Level.GravityAcceleration * Level.GravityAcceleration);
            Position = new Vector2(origin.X - (Texture.Width / 2), origin.Y - (Texture.Height / 2));
            Container = new Rectangle(0, 0, Texture.Width, Texture.Height);

            Vector2 offset = origin - target;
            var distance = offset.Length();
            Vector2 dir = offset / distance;

            Velocity = force * Vector2.Normalize(dir) * -1;

        }

    }

    public class Snowball : Projectile
    {
        public Snowball(DrawableObjectType ot) : base(ot)
        {
            Texture = SharedTexturesContainer.SharedTextures.Snowball;
        }

       

        public override float Weight
        {
            get
            {
                return 15;
            }
        }

    }
}
