using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using TerrariaCompiler.Pipeline.Converters;

namespace TerrariaCompiler.Pipeline
{
	internal class Compiler
	{
		private readonly object contentCompiler = Activator.CreateInstance(typeof(ContentCompiler), true);
		private readonly MethodInfo compileMethod = typeof(ContentCompiler).GetMethod("Compile", BindingFlags.NonPublic | BindingFlags.Instance);

		private readonly ContentProcessorContext contentProcessorContext = new ProcessorContext();
		private readonly ContentImporterContext contentImporterContext = new ImporterContext();

		private readonly IReadOnlyDictionary<string, ContentConverter> converters = GetConverters();

		private static ReadOnlyDictionary<string, ContentConverter> GetConverters()
		{
			IDictionary<string, ContentConverter> converters = new Dictionary<string, ContentConverter>();
			foreach (Type converterType in Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(ContentConverter))))
			{
				ContentConverter converter = (ContentConverter)Activator.CreateInstance(converterType);
				foreach (string extension in converter.Extensions.Split('|'))
					converters.Add(converter.Extensions, converter);
			}
			return new ReadOnlyDictionary<string, ContentConverter>(converters);
		}

		public void CompileDirectory(string directory)
		{
			Console.WriteLine($"\nSearching:\n{ directory }\nfor files.");

			foreach (string file in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
				CompileFile(file);
		}

		public void CompileFile(string file)
		{
			if (converters.TryGetValue(Path.GetExtension(file), out ContentConverter converter))
				CompileFile(converter, file);
		}

		private void CompileFile(ContentConverter converter, string originalFile)
		{
			string xnbFile = Path.ChangeExtension(originalFile, "xnb");

#if !DEBUG
			if (File.Exists(xnbFile) && File.GetLastWriteTimeUtc(xnbFile) > File.GetLastWriteTimeUtc(originalFile))
				return;
#endif

			Console.WriteLine($"\nConverting: { originalFile }");

			try
			{
				string directory = Path.GetDirectoryName(originalFile);

				object description = converter.Importer.Import(originalFile, contentImporterContext);
				object content = converter.Processor.Process(description, contentProcessorContext);

				using (FileStream fs = new FileStream(xnbFile, FileMode.Create))
					compileMethod.Invoke(
						contentCompiler,
						new object[]
						{
							fs,
							content,
							contentProcessorContext.TargetPlatform,
							contentProcessorContext.TargetProfile,
							converter.Compress,
							directory,
							directory
						}
					);
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;

				if (e is InvalidContentException ex)
					converter.LogError(originalFile, ex);
				else
					Console.WriteLine($"{ originalFile }: error: { e.Message }");

				Console.ResetColor();
			}
		}
	}
}