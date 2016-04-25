using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Common;

namespace YetiAdventure
{
    public class Player : CollidableObject
    {
        // Constants for controling horizontal movement
        public const float WalkSpeed = 3;

        // Constants for controlling vertical movement
        public const float MaxJumpTime = 0.2f;
        public const float JumpControlPower = 0.2f;
        public const float JumpSpeed = -50;


        public const int Height = 72;
        public const int Width = 64;
        private const int _frames = 4;
        private const int _maxticks = 9;



        private float jumpTime;
        protected int CurrentFrame = 0;
        protected int Tick = 0;

        private int RunAnimationFrame { get { return Height; } }
        private int IdleAnimationFrame { get { return 0; } }
        private int JumpAnimationFrame { get { return Height * 3; } }


        public Player()
            : base(DrawableObjectType.Player)
        {
            Container = new Rectangle(0, IdleAnimationFrame, Width, Height);
        }


   
        public override void Update(GameTime gameTime)
        {

            var velocityY = 0f;
            if (IsJumping)
            {
                IsOnGround = false;
                if (jumpTime == 0.0f) { /*jumpSound.Play(); */}

                jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // If we are in the ascent of the jump
                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    var jumpvar = (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower);
                    velocityY = JumpSpeed * (1.0f - jumpvar);
                }
                else
                {
                    // Reached the apex of the jump
                    jumpTime = 0.0f;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
                jumpTime = 0.0f;
            }



            if (CurrentFrame == _frames) CurrentFrame = 0;
            

            //jump animation takes priority over run
            if (Math.Abs(velocityY) - 0.02f > 0)
            {
                //jump animation
                Container = new Rectangle(0, JumpAnimationFrame, Width, Height);
                
            }
            else if (Math.Abs(Velocity.X) - 0.02f > 0)
            {
                if (Tick < _maxticks)
                    Tick++;
                if (Tick == _maxticks)
                {
                    Container = new Rectangle(CurrentFrame * Width, RunAnimationFrame, Width, Height);
                    CurrentFrame++;
                    Tick = 0;
                }
            }
            else
            {
                Container = new Rectangle(0, IdleAnimationFrame, Width, Height);
            }



            //if Y velocity is not zero then we are still jumping
            IsJumping = Math.Abs(velocityY) > 0;

            Velocity = new Vector2(Velocity.X, velocityY);
            Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
            foreach (var bullet in Bullets)
            {
                bullet.Update(gameTime);
                if (bullet.IsDead)
                    Bullets.Remove(bullet);
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0)
                SpriteEffect = SpriteEffects.None;
            else if (Velocity.X < 0)
                SpriteEffect = SpriteEffects.FlipHorizontally;


            spriteBatch.Draw(Texture, Position, Container, Color.White, 0.0f, new Vector2(), 1.0f, SpriteEffect, 0.0f);
            foreach (var bullet in Bullets)
            {
                bullet.Draw(gameTime, spriteBatch);
            }
        }

        private SpriteEffects _spriteEffect = SpriteEffects.None;

        public SpriteEffects SpriteEffect
        {
            get { return _spriteEffect; }
            set { _spriteEffect = value; }
        }


        public bool CanJump()
        {
            return !IsJumping && jumpTime == 0.0f && IsOnGround;
        }

        /// <summary>
        /// 
        /// </summary>
        public Direction Direction { get; set; }

        public void Shoot(Vector2 target)
        {
            //add a new Projectile to bullets
            //get ammo type
            var ammoType = GetAmmoType();
            var projectile = Activator.CreateInstance(ammoType, DrawableObjectType.Friendly) as Projectile;
            Bullets.Add(projectile);


        }


        private Type GetAmmoType()
        {
            //based on currently equipped weapon get ammo type
            return AmmoTypeMapper[AmmoType.Snowball];
        }

        Dictionary<AmmoType, Type> AmmoTypeMapper = new Dictionary<AmmoType, Type>()
        {
            {AmmoType.Snowball, typeof(Snowball) },
        };

        List<Projectile> _bullets;

        public List<Projectile> Bullets
        {
            get { return _bullets ?? (_bullets = new List<Projectile>()); }
        }
    }
}
