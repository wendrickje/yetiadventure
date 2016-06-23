using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Reads SpriteSheet instances from a ContentReader
	/// </summary>
	public class SpriteSheetReader
		: ContentTypeReader<SpriteSheet>
	{
		protected override SpriteSheet Read(ContentReader input, SpriteSheet existingInstance)
		{
			if (input == null) throw new ArgumentNullException("input");

			var texture = (Texture2D)input.ReadExternalReference<Texture>();

			var spriteCount = input.ReadInt32();
			var spriteEntries = Enumerable.Range(0, spriteCount).Select(x =>
			                                                            	{
			                                                            		var name = input.ReadString();
																				var pathItemsCount = input.ReadInt32();
																				var pathItems = Enumerable.Range(0, pathItemsCount).Select(y => input.ReadString()).ToArray();
																				var rectX = input.ReadInt32();
			                                                            		var rectY = input.ReadInt32();
			                                                            		var rectWidth = input.ReadInt32();
			                                                            		var rectHeight = input.ReadInt32();
			                                                            		var sprite = new Sprite(name, texture, new Microsoft.Xna.Framework.Rectangle(rectX,rectY,rectWidth,rectHeight));
			                                                            		return Tuple.Create(pathItems, sprite);
			                                                            	}).ToArray();
			var spriteSheet = existingInstance ?? new SpriteSheet(new Dictionary<string, ISpriteCategory>(), new Dictionary<string, Sprite>());
			PopulateSprites(spriteSheet, spriteEntries);
			PopulateSubCategories(spriteSheet, spriteEntries);

			return spriteSheet;
		}

		private void PopulateSprites(SpriteSheet spriteCategory, Tuple<string[], Sprite>[] spriteEntries)
		{
			if (spriteCategory == null) throw new ArgumentNullException("spriteCategory");
			if (spriteEntries == null) throw new ArgumentNullException("spriteEntries");

			var applicableSpriteEntries = spriteEntries.Where(x => x.Item1.Length == 0);
			var applicableSprites = applicableSpriteEntries.Select(x => x.Item2);
			foreach (var s in applicableSprites)
			{
				spriteCategory.SpritesWritable.Add(s.Name, s);
			}
		}
		private void PopulateSubCategories(SpriteSheet spriteCategory, Tuple<string[], Sprite>[] spriteEntries)
		{
			if (spriteCategory == null) throw new ArgumentNullException("spriteCategory");
			if (spriteEntries == null) throw new ArgumentNullException("spriteEntries");

			var fullPathsOfDirectChildrenCategories = spriteEntries.Where(x => x.Item1.Length > 0).Select(x => x.Item1.Take(1).ToArray()[0]).Distinct(StringComparer.CurrentCulture);
			foreach (var fp in fullPathsOfDirectChildrenCategories)
			{
				var newSubCategory = new SpriteCategory(fp, new Dictionary<string, ISpriteCategory>(), new Dictionary<string, Sprite>());
				spriteCategory.CategoriesWritable.Add(newSubCategory.Name, newSubCategory);
				PopulateSubCategories(newSubCategory, new [] {fp}, spriteEntries);
				PopulateSprites(newSubCategory, new [] {fp}, spriteEntries);
			}
		}

		private void PopulateSprites(SpriteCategory spriteCategory, string[] spriteCategoryParentPath, Tuple<string[], Sprite>[] spriteEntries)
		{
			if (spriteCategory == null) throw new ArgumentNullException("spriteCategory");
			if (spriteCategoryParentPath == null) throw new ArgumentNullException("spriteCategoryParentPath");
			if (spriteEntries == null) throw new ArgumentNullException("spriteEntries");

			var applicableSpriteEntries = spriteEntries.Where(x => Enumerable.SequenceEqual(spriteCategoryParentPath,x.Item1));
			var applicableSprites = applicableSpriteEntries.Select(x => x.Item2);
			foreach (var s in applicableSprites)
			{
				spriteCategory.SpritesWritable.Add(s.Name, s);
			}
		}

		private void PopulateSubCategories(SpriteCategory spriteCategory, string[] spriteCategoryParentPath, Tuple<string[], Sprite>[] spriteEntries)
		{
			if (spriteCategory == null) throw new ArgumentNullException("spriteCategory");
			if (spriteCategoryParentPath == null) throw new ArgumentNullException("spriteCategoryParentPath");
			if (spriteEntries == null) throw new ArgumentNullException("spriteEntries");

			var fullPathsOfDirectChildrenCategories = spriteEntries.Where(x => x.Item1.Length > spriteCategoryParentPath.Length 
																		&& Enumerable.SequenceEqual(spriteCategoryParentPath,x.Item1.Take(spriteCategoryParentPath.Length)))
																	.Select(x => x.Item1.Take(spriteCategoryParentPath.Length + 1).ToArray())
																	.Distinct(new StringArrayComparer());
			foreach (var fp in fullPathsOfDirectChildrenCategories)
			{
				var newSubCategory = new SpriteCategory(fp.Last(), new Dictionary<string, ISpriteCategory>(), new Dictionary<string, Sprite>());
				spriteCategory.CategoriesWritable.Add(newSubCategory.Name, newSubCategory);
				PopulateSubCategories(newSubCategory, fp, spriteEntries);
				PopulateSprites(newSubCategory, fp, spriteEntries);
			}
		}
	}
}
