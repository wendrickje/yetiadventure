using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YetiAdventure.Components;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Levels
{
    public class DemoLevel : Level
    {


        public DemoLevel()
            : base("demo")
        {
            
        }

        public override void Initalize()
        {
            base.Initalize();
        }
        

        public override void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            base.LoadContent(spriteBatch, content);


        }



    }



}
