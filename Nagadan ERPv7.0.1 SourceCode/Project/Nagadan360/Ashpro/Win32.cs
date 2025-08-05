using System;
using System.Runtime.InteropServices;

namespace Ashpro
{
	public class Win32
	{
		[DllImport("User32.dll")]
		public static extern bool LockWorkStation();

		[DllImport("User32.dll")]
		private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

		[DllImport("Kernel32.dll")]
		private static extern uint GetLastError();

		public static uint GetIdleTime()
		{
			LASTINPUTINFO plii = default(LASTINPUTINFO);
			plii.cbSize = (uint)Marshal.SizeOf(plii);
			GetLastInputInfo(ref plii);
			return (uint)Environment.TickCount - plii.dwTime;
		}

		public static long GetTickCount()
		{
			return Environment.TickCount;
		}

		public static long GetLastInputTime()
		{
			LASTINPUTINFO plii = default(LASTINPUTINFO);
			plii.cbSize = (uint)Marshal.SizeOf(plii);
			if (!GetLastInputInfo(ref plii))
			{
				throw new Exception(GetLastError().ToString());
			}
			return plii.dwTime;
		}
	}
}
