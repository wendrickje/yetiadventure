using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace Sentius.darkFunction.PipelineExtensions.Content_Types
{
	public class SpriteAnimationDefinitionSetContent
	{
		public string SpriteAnimationFileName;
		public string SpriteAnimationDirectory;

		public string SpriteSheetFile;
		public IEnumerable<AnimationDefinition> AnimationDefinitions;
		public ExternalReference<SpriteSheetContent> SpriteSheetFileReference;

		public SpriteAnimationDefinitionSetContent(XDocument xmlDocument, ContentImporterContext context)
		{
			var rootElement = xmlDocument.Element("animations");

			this.SpriteSheetFile = rootElement.Attribute("spriteSheet").Value;
			this.AnimationDefinitions = rootElement.Elements("anim").Select(xelement => new AnimationDefinition(xelement, context)).ToArray();
		}

		public class AnimationDefinition
		{
			public string Name;
			public int Loops;
			public IOrderedEnumerable<AnimationFrameDefinition> Frames;

			public AnimationDefinition(XElement xelement, ContentImporterContext context)
			{
				this.Name = xelement.Attribute("name").Value;
				this.Loops = int.Parse(xelement.Attribute("loops").Value);

				this.Frames = xelement.Elements("cell").Select(xelement2 => new AnimationFrameDefinition(xelement2, context)).ToArray().OrderBy(f => f.Index);
			}
		}

		public class AnimationFrameDefinition
		{
			public int Index;
			public int Delay;
			public IEnumerable<SpriteReference> Sprites;

			public AnimationFrameDefinition(XElement xelement, ContentImporterContext context)
			{
				this.Index = int.Parse(xelement.Attribute("index").Value);
				this.Delay = int.Parse(xelement.Attribute("delay").Value);

				this.Sprites = xelement.Elements("spr").Select(xelement2 => new SpriteReference(xelement2, context)).ToArray();
			}
		}

		public class SpriteReference
		{
			public string[] NameParts;
			public int X;
			public int Y;

			public SpriteReference(XElement xElement, ContentImporterContext context)
			{
				this.NameParts = xElement.Attribute("name").Value.Split(new char[]{'/'}, StringSplitOptions.RemoveEmptyEntries);
				this.X = int.Parse(xElement.Attribute("x").Value);
				this.Y = int.Parse(xElement.Attribute("y").Value);
			}
		}
	}
}
