using System;
using Microsoft.Xna.Framework.Content.Pipeline;
// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = System.String;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using YetiAdventure.Levels;

namespace LevelPackageFileProcessor
{
    [ContentProcessor(DisplayName = "Level Package Processor")]
    public class LevelPackageProcessor : ContentProcessor<CompiledPackage, LevelPackage>
    {

        public override LevelPackage Process(CompiledPackage input, ContentProcessorContext context)
        {
            //uncompress the pack into 
            //texture 2d
            //level config file
            //level layout file

            var spritesheetbytes = new byte[] { };
            var levelconfigbytes = new byte[] { };
            var levellayoutbytes = new byte[] { };

            
            var target = input.Raw;
            using (var memoryStream = new MemoryStream(target))
            //using (FileStream fileToDecompress = File.Open(target.FullName, FileMode.Open))
            {
                using (DeflateStream decompressionStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
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
           

            // Return the levelpackage
            return new LevelPackage(null,null,null);
        }
    }
}
