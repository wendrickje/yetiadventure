using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Levels;

namespace LevelPackageFileProcessor
{
    [ContentTypeWriter]
    public class LevelPackageContentWriter : ContentTypeWriter<CompiledPackage>
    {
        protected override void Write(ContentWriter output, CompiledPackage value)
        {
            output.Write(value.Raw.Length);
            output.Write(value.Raw);
        }
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(LevelPackage).AssemblyQualifiedName;
        }
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "LevelPackageFileReader.LevelPackageContentReader, LevelPackageFileReader";
        }
    }
}
