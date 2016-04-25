using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelPackageFileProcessor
{
    [ContentTypeWriter]
    class LevelPackageContentWriter : ContentTypeWriter<CompiledPackage>
    {
        protected override void Write(ContentWriter output, CompiledPackage value)
        {
            output.Write(value.Compiled.Length);
            output.Write(value.Compiled);
        }
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(CompiledPackage).AssemblyQualifiedName;
        }
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "LevelPackageFileProcessor.LevelPackageContentReader, LevelPackage," +
                " Version=1.0.0.0, Culture=neutral";
        }
    }
}
