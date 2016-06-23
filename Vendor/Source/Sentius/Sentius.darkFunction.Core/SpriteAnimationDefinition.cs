using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents a sprite animation loaded from a darkFunction Sprite Animation file
	/// </summary>
	public class SpriteAnimationDefinition
		: IEnumerable<SpriteAnimationFrameDefinition>
	{
		internal SpriteAnimationDefinition()
		{
			
		}

		/// <summary>
		/// Gets the name of the sprite animation
		/// </summary>
		public string Name { get; internal set; }

		internal SpriteAnimationFrameDefinition[] FramesWriteable { get; set; }

		/// <summary>
		/// Gets an enumeration of the animation frames of the animation
		/// </summary>
		public ReadOnlyCollection<SpriteAnimationFrameDefinition> Frames { get { return new ReadOnlyCollection<SpriteAnimationFrameDefinition>(FramesWriteable.ToArray()); } }

		/// <summary>
		/// Gets the calculated bounds of the animation
		/// </summary>
		public Rectangle Bounds { get; internal set; }

		#region Implementation of IEnumerable

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		IEnumerator<SpriteAnimationFrameDefinition> IEnumerable<SpriteAnimationFrameDefinition>.GetEnumerator()
		{
			return FramesWriteable.AsEnumerable().GetEnumerator();
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
			return FramesWriteable.GetEnumerator();
		}

		#endregion
	}
}