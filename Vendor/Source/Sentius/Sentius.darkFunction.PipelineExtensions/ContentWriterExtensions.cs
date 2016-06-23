using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Sentius.darkFunction.PipelineExtensions
{
	internal static class ContentWriterExtensions
	{
		internal static void WriteStringSafe(this ContentWriter output, string value, System.Text.Encoding encoding)
		{
			output.Write(value.Length);
			var bytes = encoding.GetBytes(value);
			output.Write(bytes);
		}

		internal static void WriteArray<T>(this ContentWriter output, T[] values, Action<ContentWriter, T> writeArrayItem)
		{
			output.Write(values.Length);

			for(int i = 0; i<values.Length; i++)
			{
				writeArrayItem(output,values[i]);
			}
		}

		internal static void WriteArray<T>(this ContentWriter output, T[] values, Action<T> writeArrayItem)
		{
			output.Write(values.Length);

			for(int i = 0; i<values.Length; i++)
			{
				writeArrayItem(values[i]);
			}
		}
	}
}
