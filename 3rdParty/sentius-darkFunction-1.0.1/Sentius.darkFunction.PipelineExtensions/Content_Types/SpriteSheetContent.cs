using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Sentius.darkFunction.PipelineExtensions.Content_Types
{
	public class SpriteSheetContent
	{
		public string SpriteSheetFileName;
		public string SpriteSheetDirectory;
		public string ImageFile;
		public ExternalReference<TextureContent> ImageFileReference;
		public int ImageFileWidth;
		public int ImageFileHeight;
		public List<Item> Items = new List<Item>();

		public SpriteSheetContent(XDocument xmlDocument, ContentImporterContext context)
		{
			var rootElement = xmlDocument.Element("img");
			this.ImageFile = rootElement.Attribute("name").Value;
			this.ImageFileWidth = int.Parse(rootElement.Attribute("w").Value);
			this.ImageFileHeight = int.Parse(rootElement.Attribute("h").Value);
			LoadElementChildren(rootElement.Element("definitions"), new string[] {});
		}

		private void LoadElementChildren(XElement element, string[] currentPath)
		{
			var directoriesHere = element.Elements("dir");

			foreach (var dir in directoriesHere)
			{
				var dirName = dir.Attribute("name").Value;
				string[] newPath;
				if (dirName == "/" && currentPath.Length == 0)
					newPath = currentPath;
				else
					newPath = currentPath.Concat(new[] { dirName }).ToArray();
				LoadElementChildren(dir, newPath);
			}

			var spritesHere = element.Elements("spr");
			foreach (var spr in spritesHere)
			{
				var sprName = spr.Attribute("name").Value;
				var sprX = int.Parse(spr.Attribute("x").Value);
				var sprY = int.Parse(spr.Attribute("y").Value);
				var sprWidth = int.Parse(spr.Attribute("w").Value);
				var sprHeight = int.Parse(spr.Attribute("h").Value);
				var newItem = new Item()
				              	{
									Path = currentPath,
									Name = sprName,
									X = sprX,
									Y = sprY,
									Width = sprWidth,
									Height = sprHeight
				              	};
				this.Items.Add(newItem);
			}
		}

		public class Item
		{
			public string[] Path;
			public string Name;
			public int X;
			public int Y;
			public int Width;
			public int Height;
		}
	}
}
