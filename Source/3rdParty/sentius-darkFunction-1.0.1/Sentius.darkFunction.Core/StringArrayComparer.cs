namespace Sentius.darkFunction.Core
{
	using System.Collections.Generic;
	using System.Linq;

	public class StringArrayComparer : IEqualityComparer<string[]>
	{
		public bool Equals(string[] x, string[] y) {
			return x.SequenceEqual(y);
		}

		public int GetHashCode(string[] obj) {
			return base.GetHashCode();
		}
	}
}