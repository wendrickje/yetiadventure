using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sentius.darkFunction.PipelineExtensions.Content_Types;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Sentius.darkFunction.PipelineExtensions
{
	using Sentius.darkFunction.PipelineExtensions.Content_Types;

	/// <summary>
	/// Writes a SpriteAnimationDefinitionSetContent to an XNB file for consumption by a game.
	/// </summary>
	[ContentTypeWriter]
	public class SpriteAnimationDefinitionSetWriter : ContentTypeWriter<SpriteAnimationDefinitionSetContent>
	{
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return "Sentius.darkFunction.Core.SpriteAnimationDefinitionSetReader, Sentius.darkFunction.Core";
		}

		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return "Sentius.darkFunction.Core.SpriteAnimationDefinitionSet, Sentius.darkFunction.Core";
		}

		protected override void Write(ContentWriter output, SpriteAnimationDefinitionSetContent value)
		{
			output.WriteExternalReference(value.SpriteSheetFileReference);

			output.WriteArray(value.AnimationDefinitions.ToArray(), (SpriteAnimationDefinitionSetContent.AnimationDefinition anim) =>
			{
				output.WriteStringSafe(anim.Name, System.Text.Encoding.UTF8);
				
				// NOTE: This flattens loops and frame repeats
				var allFramesFlattened = Enumerable.Repeat(
						anim.Frames.SelectMany(f=>Enumerable.Repeat(f,f.Delay + 1))
					, anim.Loops + 1).SelectMany(x => x).ToArray();

				output.WriteArray(allFramesFlattened, (frame) =>
				{
					output.WriteArray(frame.Sprites.ToArray(), (spr) => {
						output.WriteArray(spr.NameParts, s => output.WriteStringSafe(s, System.Text.Encoding.UTF8));

						output.Write(spr.X);
						output.Write(spr.Y);
					});
				});
			});
		}
	}
}
