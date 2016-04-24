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

        public override void Update(GameTime gameTime)
        {
            
            //move the projectile along until 
            //when it reaches its apex kill it
            
        }
    }

    public class Snowball : Projectile
    {
        public Snowball(DrawableObjectType ot) : base(ot)
        {

        }

        public override void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            base.LoadContent(spriteBatch, content);
            Texture = content.Load<Texture2D>("snowball");
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
