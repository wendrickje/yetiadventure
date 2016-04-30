using System;
using Microsoft.Xna.Framework.Content.Pipeline;
// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = System.String;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using YetiAdventure.Levels;
using System.Text;

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

            var divider = "====";
            var configHeader = String.Format("{0}{1}{0}", divider, ".config");
            var levelHeader = String.Format("{0}{1}{0}", divider, ".lev");

            var target = input.Raw;
            using (var memoryStream = new MemoryStream(target))
            //using (FileStream fileToDecompress = File.Open(target.FullName, FileMode.Open))
            {
                using (DeflateStream decompressionStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                {
                    var streamreader = new StreamReader(decompressionStream);
                    
                    while (!streamreader.EndOfStream)
                    {
                        //====.config====000

                        //==== .lev ====000
                        //      == layer 0 ==
                        //      == layer 1 ==
                        //      == layer 2 ==
                        //      == layer 3 ==
                        
                        //could be of any file type
                        //====.png || .jpg || .bmp====000

                        var line = streamreader.ReadLine().Trim(null);
                        //read to end of .config section
                        if (line.Contains(configHeader))
                        {
                            var configSection = String.Empty;
                            line = String.Empty;
                            while (!line.Contains(levelHeader))
                            {
                                configSection = String.Join("\r\n", configSection + line);
                                line = streamreader.ReadLine();
                            }
                            var raw = Encoding.Default.GetBytes(configSection);
                            

                        }


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
