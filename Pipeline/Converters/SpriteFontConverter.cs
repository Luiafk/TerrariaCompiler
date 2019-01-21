using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal class SpriteFontConverter : ContentConverter
	{
		public override string Extension { get; } = ".spritefont";
		public override bool Compress { get; } = true;
		public override IContentImporter Importer { get; } = new FontDescriptionImporter();
		public override IContentProcessor Processor { get; } = new FontDescriptionProcessor();

		public override void LogError(string file, InvalidContentException ex) => Console.WriteLine($"{ file } ({ ex.ContentIdentity?.FragmentIdentifier ?? "," }): error: { ex.Message }");
	}
}