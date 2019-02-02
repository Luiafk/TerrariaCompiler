using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal class SpriteFontConverter : ContentConverter
	{
		public override string Extension { get; } = ".spritefont";
		public override bool Compress { get; } = true;
		public override IContentImporter Importer { get; } = new FontDescriptionImporter();
		public override IContentProcessor Processor { get; } = new FontDescriptionProcessor();
	}
}