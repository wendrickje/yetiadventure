using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sentius.darkFunction.PipelineExtensions.Content_Types;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace Sentius.darkFunction.PipelineExtensions
{
	using Sentius.darkFunction.PipelineExtensions.Content_Types;

	[ContentImporter(".anim", DisplayName = "darkFunction Sprite Animation Importer", DefaultProcessor = "SpriteAnimationDefinitionSetProcessor")]
	public class SpriteAnimationDefinitionSetImporter
		: ContentImporter<SpriteAnimationDefinitionSetContent>
	{
		public override SpriteAnimationDefinitionSetContent Import(string filename, ContentImporterContext context)
		{
            // load the xml
            XDocument doc = XDocument.Load(filename);

			// create the content from the xml
			SpriteAnimationDefinitionSetContent definitionSetContent = new SpriteAnimationDefinitionSetContent(doc, context);

			// save the filename and directory for the processor to use
			definitionSetContent.SpriteAnimationFileName = filename;
            definitionSetContent.SpriteAnimationDirectory = filename.Remove(filename.LastIndexOf('/'));

			return definitionSetContent;
		}
	}
}
