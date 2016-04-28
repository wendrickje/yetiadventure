using System.Collections.Generic;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents a sprite folder that may contain sprites or deeper sprite folders
	/// </summary>
	public interface ISpriteCategory
		: ISpriteSheetNode
	{
		/// <summary>
		/// Gets the dictionary of sprites that are contained by the current sprite folder
		/// </summary>
		IDictionary<string, Sprite> Sprites { get; }

		/// <summary>
		/// Gets the dictionary of sprite folders that are contained by the current sprite folder
		/// </summary>
		IDictionary<string, ISpriteCategory> Categories { get; }
	}
}