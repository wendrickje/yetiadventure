using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sentius.darkFunction.PipelineExtensions.Content_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Sentius.darkFunction.PipelineExtensions
{
	using Sentius.darkFunction.PipelineExtensions.Content_Types;

	/// <summary>
	/// Writes a SpriteSheetContent to an XNB file for consumption by a game.
	/// </summary>
	[ContentTypeWriter]
	public class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
	{
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return "Sentius.darkFunction.Core.SpriteSheetReader, Sentius.darkFunction.Core";
		}

		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return "Sentius.darkFunction.Core.SpriteSheet, Sentius.darkFunction.Core";
		}

		protected override void Write(ContentWriter output, SpriteSheetContent value)
		{
			output.WriteExternalReference(value.ImageFileReference);
			
			output.Write(value.Items.Count);
			foreach (var i in value.Items)
			{
				output.Write(i.Name);
				output.Write(i.Path.Length);
				i.Path.ToList().ForEach(output.Write);

				output.Write(i.X);
				output.Write(i.Y);
				output.Write(i.Width);
				output.Write(i.Height);
			}
		}
	}
}
