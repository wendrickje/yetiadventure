using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Reads SpriteAnimationDefinitionSet instances from a ContentReader
	/// </summary>
	public class SpriteAnimationDefinitionSetReader
		: ContentTypeReader<SpriteAnimationDefinitionSet>
	{
		protected override SpriteAnimationDefinitionSet Read(ContentReader input, SpriteAnimationDefinitionSet existingInstance)
		{
			var instance = existingInstance ?? new SpriteAnimationDefinitionSet();

			var spriteSheet = input.ReadExternalReference<SpriteSheet>();

			var newAnimations = input.ReadArray(() =>
			{
				var newAnimation = new SpriteAnimationDefinition();

				newAnimation.Name = input.ReadStringSafe(System.Text.Encoding.UTF8);

				var originalFrames = input.ReadArray(() => { 
					var newFrame = new SpriteAnimationFrameDefinition();

					newFrame.SpriteReferencesWriteable = input.ReadArray(() => {
						var newSpriteReference = new SpriteAnimationFrameDefinition.SpriteReference();

						var nameParts = input.ReadArray(() => input.ReadStringSafe(System.Text.Encoding.UTF8));

						newSpriteReference.Sprite = LookupSprite(spriteSheet, nameParts);

						var x = input.ReadInt32();
						var y = input.ReadInt32();
						newSpriteReference.Position = new Microsoft.Xna.Framework.Point(x, y);

						return newSpriteReference;
					});

					newFrame.Bounds = AggregateBoundsRect(newFrame.SpriteReferences.Select(sr => new Rectangle(sr.Position.X, sr.Position.Y, sr.Sprite.SourceRectangle.Width, sr.Sprite.SourceRectangle.Height)));

					return newFrame;
				});

				newAnimation.FramesWriteable = originalFrames;
				newAnimation.Bounds = AggregateBoundsRect(originalFrames.Select(f => f.Bounds));

				return newAnimation;
			});

            foreach (SpriteAnimationDefinition animDefinition in newAnimations.ToList())
            {
                instance.AnimationDefinitionsWritable.Add(animDefinition.Name, animDefinition);
            }

			return instance;
		}

		private static Sprite LookupSprite(ISpriteCategory spriteSheet, IEnumerable<string> spriteNameParts)
		{
			if (spriteSheet == null) throw new ArgumentNullException("spriteSheet");
			if (spriteNameParts == null) throw new ArgumentNullException("spriteNameParts");

			var nextName = spriteNameParts.First();
			var remainingNames = spriteNameParts.Skip(1);

			if (remainingNames.Count() == 0)
				return spriteSheet.Sprites[nextName];
			else
				return LookupSprite(spriteSheet.Categories[nextName], remainingNames);
		}

		private static Rectangle AggregateBoundsRect(IEnumerable<Rectangle> rectangles)
		{
			var minX = rectangles.Min(r => r.Left);
			var maxX = rectangles.Max(r => r.Right);

			var minY = rectangles.Min(r => r.Top);
			var maxY = rectangles.Max(r => r.Bottom);

			return new Rectangle(minX, minY, maxX - minX, maxY - minY);
		}
	}

	internal static class ContentReaderExtensions
	{
		internal static string ReadStringSafe(this ContentReader input, System.Text.Encoding encoding)
		{
			var length = input.ReadInt32();
			var bytes = input.ReadBytes(length);

			return encoding.GetString(bytes,0,bytes.Length);
		}

		internal static T[] ReadArray<T>(this ContentReader input, Func<ContentReader,T> readArrayItem)
		{
			var length = input.ReadInt32();
			List<T> resultItems = new List<T>();
			for(int i = 0; i<length; i++)
			{
				resultItems.Add(readArrayItem(input));
			}

			return resultItems.ToArray();
		}

		internal static T[] ReadArray<T>(this ContentReader input, Func<T> readArrayItem)
		{
			var length = input.ReadInt32();
			List<T> resultItems = new List<T>();
			for(int i = 0; i<length; i++)
			{
				resultItems.Add(readArrayItem());
			}

			return resultItems.ToArray();
		}
	}
}
