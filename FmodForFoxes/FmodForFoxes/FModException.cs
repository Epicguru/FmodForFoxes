using FMOD;

namespace FmodForFoxes;

/// <summary>
/// An exception thrown when an FMOD function returns an error code.
/// </summary>
public sealed class FModException : Exception
{
	/// <summary>
	/// The result, containing the error code.
	/// </summary>
	public RESULT Result { get; }
	
	public FModException(in RESULT result, string? expression) 
		:base(expression == null ? $"FMOD error: {FMOD.Error.String(result)}" : $"FMOD error: {FMOD.Error.String(result)} ({expression})")
	{
		Result = result;
	}
}
