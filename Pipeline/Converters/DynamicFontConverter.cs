using Microsoft.Xna.Framework.Content.Pipeline;
using ReLogic.Content.Pipeline;
using System;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal class DynamicFontConverter : ContentConverter
	{
		public override string Extension { get; } = ".dynamicfont";
		public override bool Compress { get; } = true;
		public override IContentImporter Importer { get; } = (IContentImporter)Activator.CreateInstance(typeof(DynamicFontDescription).Assembly.GetType("ReLogic.Content.Pipeline.DynamicFontImporter"));
		public override IContentProcessor Processor { get; } = new DynamicFontProcessor();

		public override void LogError(string file, InvalidContentException ex) => Console.WriteLine($"{ file } ({ ex.ContentIdentity?.FragmentIdentifier ?? "," }): error: { ex.Message }");
	}
}