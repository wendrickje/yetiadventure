using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelPackageFileProcessor
{
    [ContentImporter(".lpfx", DefaultProcessor = "LevelPackageProcessor", DisplayName = "Level Package Importer")]
    public class LevelPackageImporter : ContentImporter<CompiledPackage>
    {
        public override CompiledPackage Import(string filename, ContentImporterContext context)
        {
            
            var source = System.IO.File.ReadAllBytes(filename);
            return new CompiledPackage(source);
        }
    }
}
