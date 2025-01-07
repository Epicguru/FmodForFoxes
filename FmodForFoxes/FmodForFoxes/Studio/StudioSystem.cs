using FMOD;
using FMOD.Studio;

namespace FmodForFoxes.Studio
{
	public static class StudioSystem
	{
		/// <summary>
		/// FMOD studio sound system.
		/// </summary>
		public static FMOD.Studio.System Native;

		/// <summary>
		/// Loads bank from file with custom flags.
		/// </summary>
		public static Bank LoadBank(string path, LOAD_BANK_FLAGS flags = LOAD_BANK_FLAGS.NORMAL)
		{
			var buffer = FileLoader.LoadFileAsBuffer(path);
			return LoadBank(buffer, flags);
		}

		/// <summary>
		/// Loads bank from stream with custom flags.
		/// </summary>
		public static Bank LoadBank(Stream stream, LOAD_BANK_FLAGS flags = LOAD_BANK_FLAGS.NORMAL)
		{
			var buffer = FileLoader.LoadFileAsBuffer(stream);
			return LoadBank(buffer, flags);
		}

		/// <summary>
		/// Loads bank from buffer with custom flags.
		/// </summary>
		public static Bank LoadBank(byte[] buffer, LOAD_BANK_FLAGS flags = LOAD_BANK_FLAGS.NORMAL)
		{
			Native.loadBankMemory(
				buffer,
				flags,
				out FMOD.Studio.Bank bank
			).ThrowIfNotOk();

			return new Bank(bank);
		}

		/// <summary>
		/// Retrieves an event via internal path, i.e. "event:/UI/Cancel", or ID string, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}".
		/// </summary>
		public static EventDescription GetEvent(string path)
		{
			Native.getEvent(path, out FMOD.Studio.EventDescription eventDescription).ThrowIfNotOk();
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// Retrieves an event via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static EventDescription GetEvent(GUID id)
		{
			Native.getEventByID(id, out FMOD.Studio.EventDescription eventDescription).ThrowIfNotOk();
			return new EventDescription(eventDescription);
		}

		/// <summary>
		/// Retrieves a bus using path.
		/// Path may be a path, such as bus:/SFX/Ambience, or an ID string, such as {d9982c58-a056-4e6c-b8e3-883854b4bffb}.
		/// </summary>
		public static Bus GetBus(string path)
		{
			Native.getBus(path, out var bus).ThrowIfNotOk();
			return new Bus(bus);
		}

		/// <summary>
		/// Retrieves a bus via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{2a3e48e6-94fc-4363-9468-33d2dd4d7b00}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static Bus GetBusByID(GUID id)
		{
			Native.getBusByID(id, out var bus).ThrowIfNotOk();
			return new Bus(bus);
		}


		/// <summary>
		/// Retrieves a VCA via internal path, i.e. "vca:/MyVCA", or ID string, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}".
		/// </summary>
		public static VCA GetVCA(string path)
		{
			Native.getVCA(path, out var vca).ThrowIfNotOk();
			return new VCA(vca);
		}

		/// <summary>
		/// Retrieves a VCA via 128-bit GUID.
		/// To parse a GUID from a string id, i.e. "{d9982c58-a056-4e6c-b8e3-883854b4bffb}", use FMOD.Studio.Util.parseID().
		/// </summary>
		public static VCA GetVCA(GUID id)
		{
			Native.getVCAByID(id, out var vca).ThrowIfNotOk();
			return new VCA(vca);
		}

		/// <summary>
		/// Retrieves a global parameter description by its name.
		/// </summary>
		public static PARAMETER_DESCRIPTION GetParameterDescription(string name)
		{
			Native.getParameterDescriptionByName(name, out PARAMETER_DESCRIPTION parameter).ThrowIfNotOk();
			return parameter;
		}

		/// <summary>
		/// Retrieves a global parameter description by its ID.
		/// </summary>
		public static PARAMETER_DESCRIPTION GetParameterDescription(PARAMETER_ID id)
		{
			Native.getParameterDescriptionByID(id, out PARAMETER_DESCRIPTION parameter).ThrowIfNotOk();
			return parameter;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterTargetValue(string name)
		{
			Native.getParameterByName(name, out var value, out _).ThrowIfNotOk();
			return value;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its ID.
		/// This ignores modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterTargetValue(PARAMETER_ID id)
		{
			Native.getParameterByID(id, out var value, out _).ThrowIfNotOk();
			return value;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its name (case sensitive).
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterCurrentValue(string name)
		{
			Native.getParameterByName(name, out _, out var finalValue).ThrowIfNotOk();
			return finalValue;
		}

		/// <summary>
		/// Retrieves a global parameter's current value via its ID.
		/// This takes into account modulation / automation applied to the parameter within Studio.
		/// </summary>
		public static float GetParameterCurrentValue(PARAMETER_ID id)
		{
			Native.getParameterByID(id, out _, out var finalValue).ThrowIfNotOk();
			return finalValue;
		}

		/// <summary>
		/// Sets a global parameter's value via its name (case sensitive).
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(string name, float value, bool ignoreSeekSpeed = false) =>
			Native.setParameterByName(name, value, ignoreSeekSpeed).ThrowIfNotOk();

		/// <summary>
		/// Sets a global parameter's value via its ID.
		/// Enable ignoreSeekSpeed to set the value instantly, ignoring the parameter's seek speed.
		/// </summary>
		public static void SetParameterValue(PARAMETER_ID id, float value, bool ignoreSeekSpeed = false) =>
			Native.setParameterByID(id, value, ignoreSeekSpeed).ThrowIfNotOk();

		/// <summary>
		/// Sets multiple global parameters' values via their IDs.
		/// Enable ignoreSeekSpeed to set the values instantly, ignoring the parameters' seek speeds.
		/// </summary>
		public static void SetParameterValues(PARAMETER_ID[] ids, float[] values, bool ignoreSeekSpeed = false) =>
			Native.setParametersByIDs(ids, values, ids.Length, ignoreSeekSpeed).ThrowIfNotOk();
		
	}
}
