using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sentius.darkFunction.Core
{
	/// <summary>
	/// Represents a set of sprite animations loaded from a darkFunction Sprite Animation file
	/// </summary>
	public class SpriteAnimationDefinitionSet
		: IDictionary<string,SpriteAnimationDefinition>
	{
		internal Dictionary<string, SpriteAnimationDefinition> AnimationDefinitionsWritable { get; set; }

		internal SpriteAnimationDefinitionSet()
		{
			AnimationDefinitionsWritable = new Dictionary<string, SpriteAnimationDefinition>();
		}

		#region IDictionary<string,SpriteAnimationDefinition> Members

		void IDictionary<string,SpriteAnimationDefinition>.Add(string key, SpriteAnimationDefinition value)
		{
			throw new NotSupportedException("This dictionary is read-only");
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
		/// </summary>
		/// <returns>
		/// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
		/// </returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
		public bool ContainsKey(string key)
		{
			return AnimationDefinitionsWritable.ContainsKey(key);
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
		/// </returns>
		public ICollection<string> Keys
		{
			get { return AnimationDefinitionsWritable.Keys; }
		}

		bool IDictionary<string, SpriteAnimationDefinition>.Remove(string key)
		{
			throw new NotSupportedException("This dictionary is read-only");
		}

		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <returns>
		/// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
		/// </returns>
		/// <param name="key">The key whose value to get.</param><param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
		public bool TryGetValue(string key, out SpriteAnimationDefinition value)
		{
			return AnimationDefinitionsWritable.TryGetValue(key, out value);
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
		/// </returns>
		public ICollection<SpriteAnimationDefinition> Values
		{
			get { return AnimationDefinitionsWritable.Values; }
		}

		/// <summary>
		/// Gets or sets the element with the specified key.
		/// </summary>
		/// <returns>
		/// The element with the specified key.
		/// </returns>
		/// <param name="key">The key of the element to get or set.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> is not found.</exception><exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.</exception>
		public SpriteAnimationDefinition this[string key]
		{
			get
			{
				return AnimationDefinitionsWritable[key];
			}
			set
			{
				throw new NotSupportedException("This dictionary is read-only");
			}
		}

		#endregion

		#region ICollection<KeyValuePair<string,SpriteAnimationDefinition>> Members

		void ICollection<KeyValuePair<string, SpriteAnimationDefinition>>.Add(KeyValuePair<string, SpriteAnimationDefinition> item)
		{
			throw new NotSupportedException("This dictionary is read-only");
		}

		void ICollection<KeyValuePair<string, SpriteAnimationDefinition>>.Clear()
		{
			throw new NotSupportedException("This dictionary is read-only");
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
		/// </summary>
		/// <returns>
		/// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
		/// </returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
		public bool Contains(KeyValuePair<string, SpriteAnimationDefinition> item)
		{
			return AnimationDefinitionsWritable.Contains(item);
		}

		/// <summary>
		/// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.-or-Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
		public void CopyTo(KeyValuePair<string, SpriteAnimationDefinition>[] array, int arrayIndex)
		{
			((IDictionary<string,SpriteAnimationDefinition>)AnimationDefinitionsWritable).CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <returns>
		/// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </returns>
		public int Count
		{
			get { return AnimationDefinitionsWritable.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		/// </summary>
		/// <returns>
		/// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
		/// </returns>
		public bool IsReadOnly
		{
			get { return true; }
		}

		bool ICollection<KeyValuePair<string, SpriteAnimationDefinition>>.Remove(KeyValuePair<string, SpriteAnimationDefinition> item)
		{
			throw new NotSupportedException("This dictionary is read-only");
		}

		#endregion

		#region IEnumerable<KeyValuePair<string,SpriteAnimationDefinition>> Members

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<KeyValuePair<string, SpriteAnimationDefinition>> GetEnumerator()
		{
			return AnimationDefinitionsWritable.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (AnimationDefinitionsWritable as System.Collections.IEnumerable).GetEnumerator();
		}

		#endregion
	}
}
