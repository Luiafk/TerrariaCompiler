using Microsoft.Xna.Framework.Content.Pipeline;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal abstract class ContentConverter
	{
		public abstract string Extension { get; }
		public abstract bool Compress { get; }
		public abstract IContentImporter Importer { get; }
		public abstract IContentProcessor Processor { get; }

		public abstract void LogError(string file, InvalidContentException ex);
	}
}