using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Common
{
    public class SharedTexturesContainer
    {
        private SharedTexturesContainer(ContentManager content)
        {
           
            Snowball = content.Load<Texture2D>("snowball");
        }

        public static void Create(ContentManager content)
        {
            SharedTextures = new SharedTexturesContainer(content);
            
        }

        public static SharedTexturesContainer SharedTextures { get; private set; }


        public Texture2D Snowball { get; set; }

    }
}
