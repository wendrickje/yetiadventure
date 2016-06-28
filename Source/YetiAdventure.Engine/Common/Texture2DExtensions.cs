using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.Engine.Common
{
    public static class Texture2DExtensions
    {
        public static Texture2D GetTexture(this Icon icon, GraphicsDevice graphics)
        {
            var source = icon.Raw;
            using (var stream = new MemoryStream(source))
            {
                var texture = Texture2D.FromStream(graphics, stream);
                return texture;
            }
        }
    }
}
