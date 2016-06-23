using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents a loaded darkFunction Sprite Sheet file
	/// </summary>
	public sealed class SpriteSheet
		: ISpriteCategory
	{
		private readonly ReadOnlyDictionary<string, ISpriteCategory> _Categories;
		private readonly ReadOnlyDictionary<string, Sprite> _Sprites;

		internal SpriteSheet(Dictionary<string,ISpriteCategory> categories, Dictionary<string,Sprite> sprites) 
		{
			if (categories == null) throw new ArgumentNullException("categories");
			if (sprites == null) throw new ArgumentNullException("sprites");

			this._Categories = new ReadOnlyDictionary<string, ISpriteCategory>(categories);
			this._Sprites = new ReadOnlyDictionary<string, Sprite>(sprites);
			this.CategoriesWritable = categories;
			this.SpritesWritable = sprites;
		}

		/// <summary>
		/// Gets the dictionary of sprites that are contained by the current sprite folder
		/// </summary>
		public IDictionary<string,Sprite> Sprites
		{
			get { return _Sprites; }
		}

		/// <summary>
		/// Gets the dictionary of sprite folders that are contained by the current sprite folder
		/// </summary>
		public IDictionary<string,ISpriteCategory> Categories
		{
			get { return _Categories; }
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

		string ISpriteSheetNode.Name
		{
			get { return String.Empty; }
		}
	}
}
