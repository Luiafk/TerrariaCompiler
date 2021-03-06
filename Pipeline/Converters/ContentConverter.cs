﻿using Microsoft.Xna.Framework.Content.Pipeline;
using System;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal abstract class ContentConverter
	{
		public abstract string Extensions { get; }
		public abstract bool Compress { get; }
		public abstract IContentImporter Importer { get; }
		public abstract IContentProcessor Processor { get; }

		public virtual void LogError(string file, InvalidContentException ex) => Console.WriteLine($"{ file } ({ ex.ContentIdentity?.FragmentIdentifier ?? "0,0" }): error: { ex.Message }");
	}
}