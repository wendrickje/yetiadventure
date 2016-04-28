using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Levels;

namespace LevelPackageFileReader
{
    public class LevelPackageContentReader : ContentTypeReader<LevelPackage>
    {
        protected override LevelPackage Read(ContentReader input, LevelPackage existingInstance)
        {
            return new LevelPackage(null, null, null);
        }
    }
}
