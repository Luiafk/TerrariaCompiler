using Microsoft.Xna.Framework.Content.Pipeline;
using System;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal abstract class ContentConverter
	{
		public abstract string Extension { get; }
		public abstract bool Compress { get; }
		public abstract IContentImporter Importer { get; }
		public abstract IContentProcessor Processor { get; }

		public virtual void LogError(string file, InvalidContentException ex) => Console.WriteLine($"{ file } ({ ex.ContentIdentity?.FragmentIdentifier ?? "," }): error: { ex.Message }");
	}
}