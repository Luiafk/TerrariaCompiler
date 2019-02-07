using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System;

namespace TerrariaCompiler.Pipeline.Converters
{
	internal class EffectConverter : ContentConverter
	{
		public override string Extensions { get; } = ".fx";
		public override bool Compress { get; } = false;
		public override IContentImporter Importer { get; } = new EffectImporter();
		public override IContentProcessor Processor { get; } = new EffectProcessor();

		public override void LogError(string file, InvalidContentException ex)
		{
			string error = ex.Message.Substring(ex.Message.IndexOf('\n') + 1).Replace("\r\n", " ");

			if (!error.StartsWith("("))
			{
				if (ex.ContentIdentity?.FragmentIdentifier != null)
					error = $"({ ex.ContentIdentity.FragmentIdentifier }): { error }";
				else
					error = $"(0,0): error: { error }";
			}

			Console.WriteLine($"{ file } { error }");
		}
	}
}