using Microsoft.Xna.Framework;

namespace FmodForFoxes
{
	public static class FileLoader
	{
		public static string RootDirectory { get; set; } = "";

		/// <summary>
		/// Loads file as a byte array.
		/// </summary>
		public static byte[] LoadFileAsBuffer(string path)
		{
			// NOTE: Use this method to load audio files from memory 
			// instead of built-in methods which load files directly.
			// They will not work on some platforms.

			// TitleContainer is cross-platform Monogame file loader.
			using var stream = TitleContainer.OpenStream(Path.Combine(RootDirectory, path));

			return LoadFileAsBuffer(stream);
		}

		/// <summary>
		/// Loads entire stream as a byte array.
		/// </summary>
		public static byte[] LoadFileAsBuffer(Stream stream)
		{
			using var memStream = new MemoryStream();
			stream.CopyTo(memStream);
			return memStream.ToArray();
		}
	}
}
