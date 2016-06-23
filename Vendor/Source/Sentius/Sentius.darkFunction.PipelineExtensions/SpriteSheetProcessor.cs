using System.ComponentModel;
using System.IO;
using Sentius.darkFunction.PipelineExtensions.Content_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System.Diagnostics;

namespace Sentius.darkFunction.PipelineExtensions
{
	[ContentProcessor(DisplayName = "darkFunction SpriteSheet Processor")]
    public class SpriteSheetProcessor : ContentProcessor<SpriteSheetContent, SpriteSheetContent>
    {
		[DisplayName("Color Key")]
		[DefaultValue(null)]
		[Description("The color to treat as transparent")]
		public Color? ColorKey { get; set; }


		public override SpriteSheetContent Process(SpriteSheetContent input, ContentProcessorContext context)
		{
            var path = Path.Combine(input.SpriteSheetDirectory, input.ImageFile);

			// the asset name is the entire path, minus extension, after the content directory
			string asset = string.Empty;
			if (path.StartsWith(Directory.GetCurrentDirectory()))
				asset = path.Substring(Directory.GetCurrentDirectory().Length + 1);
			else
				asset = Path.GetFileName(path);

			// build the asset as an external reference
			OpaqueDataDictionary data = new OpaqueDataDictionary();
			data.Add("GenerateMipmaps", false);
			data.Add("ResizeToPowerOfTwo", false);
			data.Add("TextureFormat", TextureProcessorOutputFormat.Color);
			data.Add("ColorKeyEnabled", ColorKey.HasValue);
			data.Add("ColorKeyColor", ColorKey.HasValue ? ColorKey.Value : Color.Magenta);
			input.ImageFileReference = context.BuildAsset<TextureContent, TextureContent>(
				new ExternalReference<TextureContent>(path),
				"TextureProcessor",
				data,
				"TextureImporter",
				asset);
			
			return input;
		}
    }
}
