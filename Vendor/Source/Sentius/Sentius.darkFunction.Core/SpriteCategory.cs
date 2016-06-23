using System;
using System.Collections.Generic;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents a folder of sprites from a darkFunction Sprite Sheet file
	/// </summary>
	public class SpriteCategory
		: ISpriteCategory
	{
		private ReadOnlyDictionary<string, Sprite> sprites;
		private IDictionary<string, ISpriteCategory> categories;

		protected internal SpriteCategory(string name, Dictionary<string,ISpriteCategory> subCategories, Dictionary<string,Sprite> sprites) 
		{
			if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
			if (subCategories == null) throw new ArgumentNullException("subCategories");
			if (sprites == null) throw new ArgumentNullException("sprites");

			Name = name;
			this.CategoriesWritable = subCategories;
			this.SpritesWritable = sprites;
		}

		internal Dictionary<string, ISpriteCategory> CategoriesWritable
		{
			get;
			private set;
		}

		internal Dictionary<string, Sprite> SpritesWritable
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the dictionary of sprites that are contained by the current sprite folder
		/// </summary>
		public IDictionary<string, Sprite> Sprites
		{
			get
			{
				if (this.sprites == null)
					this.sprites = new ReadOnlyDictionary<string, Sprite>(this.SpritesWritable);
				return this.sprites;
			}
		}

		/// <summary>
		/// Gets the dictionary of sprite folders that are contained by the current sprite folder
		/// </summary>
		public IDictionary<string, ISpriteCategory> Categories
		{
			get
			{
				if (this.categories == null)
					this.categories = new ReadOnlyDictionary<string, ISpriteCategory>(this.CategoriesWritable);
				return this.categories;
			}
		}

		/// <summary>
		/// Gets the name of the sprite folder
		/// </summary>
		public string Name
		{
			get;
			private set;
		}
	}
}