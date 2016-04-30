using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Sentius.darkFunction.PipelineExtensions.Content_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Sentius.darkFunction.PipelineExtensions
{
	using Sentius.darkFunction.PipelineExtensions.Content_Types;

	[ContentProcessor(DisplayName = "darkFunction Sprite Animation Processor")]
	public class SpriteAnimationDefinitionSetProcessor
		: ContentProcessor<SpriteAnimationDefinitionSetContent, SpriteAnimationDefinitionSetContent>
	{
		public override SpriteAnimationDefinitionSetContent Process(SpriteAnimationDefinitionSetContent input, ContentProcessorContext context)
		{
			var path = Path.Combine(input.SpriteAnimationDirectory, input.SpriteSheetFile);

			// the asset name is the entire path, minus extension, after the content directory
			string asset = string.Empty;
			if (path.StartsWith(Directory.GetCurrentDirectory()))
				asset = path.Substring(Directory.GetCurrentDirectory().Length + 1);
			else
				asset = Path.GetFileName(path);

			// build the asset as an external reference
			OpaqueDataDictionary data = new OpaqueDataDictionary();
			//data.Add("GenerateMipmaps", false);
			//data.Add("ResizeToPowerOfTwo", false);
			//data.Add("TextureFormat", TextureProcessorOutputFormat.Color);
			//data.Add("ColorKeyEnabled", ColorKey.HasValue);
			//data.Add("ColorKeyColor", ColorKey.HasValue ? ColorKey.Value : Microsoft.Xna.Framework.Color.Magenta);
			input.SpriteSheetFileReference = context.BuildAsset<SpriteSheetContent, SpriteSheetContent>(
				new ExternalReference<SpriteSheetContent>(path),
				"SpriteSheetProcessor",
				data,
				"SpriteSheetImporter",
				asset);

			return input;
		}
	}
}
