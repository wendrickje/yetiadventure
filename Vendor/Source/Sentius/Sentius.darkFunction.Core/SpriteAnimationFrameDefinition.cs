using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents an animation frame within a sprite animation
	/// </summary>
	public class SpriteAnimationFrameDefinition
		: IEnumerable<SpriteAnimationFrameDefinition.SpriteReference>
	{
		internal SpriteAnimationFrameDefinition()
		{
		}
		
		/// <summary>
		/// Gets the calculated bounds of the animation frame
		/// </summary>
		public Rectangle Bounds { get; internal set; }

		internal SpriteReference[] SpriteReferencesWriteable { get; set; }

		/// <summary>
		/// Gets an enumeration of the sprite references contained by the sprite animation frame
		/// </summary>
		public ReadOnlyCollection<SpriteReference> SpriteReferences { get { return new ReadOnlyCollection<SpriteReference>(SpriteReferencesWriteable); } }

		/// <summary>
		/// Represents a sprite references within a sprite animation frame
		/// </summary>
		public class SpriteReference
		{
			internal SpriteReference()
			{
			}

			/// <summary>
			/// Gets the sprite referenced
			/// </summary>
			public Sprite Sprite { get; internal set; }

			/// <summary>
			/// Gets the position the sprite should be rendered at, relative to the animation origin
			/// </summary>
			public Point Position { get; internal set; }
		}

		#region Implementation of IEnumerable

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		IEnumerator<SpriteReference> IEnumerable<SpriteReference>.GetEnumerator()
		{
			return SpriteReferencesWriteable.AsEnumerable().GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return SpriteReferences.GetEnumerator();
		}

		#endregion
	}
}