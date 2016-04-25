using System;
using Microsoft.Xna.Framework.Content.Pipeline;
// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = System.String;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;

namespace LevelPackageFileProcessor
{
    [ContentProcessor(DisplayName = "Level Package Processor")]
    class Processor : ContentProcessor<LevelPackage, CompiledPackage>
    {
        public override CompiledPackage Process(LevelPackage input,
            ContentProcessorContext context)
        {
            //uncompress the pack into 
            //texture 2d
            //level config file
            //level layout file

            var spritesheetbytes = new byte[] { };
            var levelconfigbytes = new byte[] { };
            var levellayoutbytes = new byte[] { };

            
            var target = new FileInfo(input.File);

            using (FileStream fileToDecompress = File.Open(target.FullName, FileMode.Open))
            {
                using (DeflateStream decompressionStream = new DeflateStream(fileToDecompress, CompressionMode.Decompress))
                {
                    var streamreader = new StreamReader(decompressionStream);
                    while (!streamreader.EndOfStream)
                    {
                        //== .lev ==
                        //      == layer {0} ==
                        //== .lev.config ==
                        //.png || .jpg || .bmp
                        var line = streamreader.ReadLine().Trim(null);
                        Debug.WriteLine("decompressed: " + line);
                    }
                }
            }

            

            //EffectContent content = new EffectContent();
            //content.EffectCode = input.SourceCode;
            //EffectProcessor compiler = new EffectProcessor();
            //CompiledEffectContent compiledContent = compiler.Process(content, context);
            var bytes = new byte[]{};

            // Return the compiled effect code portion only
            return new CompiledPackage(bytes);
        }
    }
}
