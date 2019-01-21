using System;
using System.IO;
using TerrariaCompiler.Pipeline;

namespace TerrariaCompiler
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Compiler compiler = new Compiler();
			if (args.Length > 0)
			{
				if (File.Exists(args[0]))
					compiler.CompileFile(args[0]);
				else if (Directory.Exists(args[0]))
					compiler.CompileDirectory(args[0]);
				else
					Console.WriteLine($"error: Cannot find file or directory: { args[0] }");
			}
			else
				compiler.CompileDirectory(Directory.GetCurrentDirectory());

			Console.WriteLine("\nFinished!");
		}
	}
}