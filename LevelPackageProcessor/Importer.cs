using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelPackageFileProcessor
{
    [ContentImporter(".lpfx", DefaultProcessor = "Processor", DisplayName = "Level Package Importer")]
    class Importer : ContentImporter<LevelPackage>
    {
        public override LevelPackage Import(string filename, ContentImporterContext context)
        {
            string sourceCode = System.IO.File.ReadAllText(filename);
            return new LevelPackage(System.IO.Path.GetFileNameWithoutExtension(filename));
        }
    }
}
