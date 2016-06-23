using System.Xml.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace Sentius.darkFunction.PipelineExtensions
{
    using Sentius.darkFunction.PipelineExtensions.Content_Types;
    using System.Diagnostics;
    [ContentImporter(".sprites", DisplayName = "darkFunction SpriteSheet Importer", DefaultProcessor = "SpriteSheetProcessor")]
	public class SpriteSheetImporter : ContentImporter<SpriteSheetContent>
	{
		public override SpriteSheetContent Import(string filename, ContentImporterContext context)
		{
			// load the xml
			XDocument doc = XDocument.Load(filename);

			// create the content from the xml
			SpriteSheetContent content = new SpriteSheetContent(doc, context);

			// save the filename and directory for the processor to use
			content.SpriteSheetFileName = filename;
			content.SpriteSheetDirectory = filename.Remove(filename.LastIndexOf('/'));

			return content;
		}
	}
}
