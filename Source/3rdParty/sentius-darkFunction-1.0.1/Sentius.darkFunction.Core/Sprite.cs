using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents a sprite loaded from a darkFunction sprite sheet
	/// </summary>
	public sealed class Sprite
		: ISpriteSheetNode
	{
		/// <summary>
		/// Gets the name of the sprite
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the texture that the sprite can be found on
		/// </summary>
		public Texture2D SourceTexture { get; private set; }
		/// <summary>
		/// Gets the rectangle that the sprite can be found within on the <see cref="SourceTexture">SourceTexture</see>
		/// </summary>
		public Rectangle SourceRectangle { get; private set; }

		internal Sprite(string name, Texture2D texture, Rectangle rectangle)
		{
			if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
			if (texture == null) throw new ArgumentNullException("texture");

			Name = name;
			SourceTexture = texture;
			SourceRectangle = rectangle;
		}
	}
}