using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sentius.darkFunction.Core
{
#if WINDOWS_PHONE || XBOX
	// NOTE: This type is built into .NET 4, which means we don't have it when we target Xbox or Windows Phone

	public struct Tuple<T1,T2>
	{
		public T1 Item1;
		public T2 Item2;
	}

	public static class Tuple
	{
		public static Tuple<T1,T2> Create<T1,T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>() { Item1 = item1, Item2 = item2 };
		}
	}
#endif
}
