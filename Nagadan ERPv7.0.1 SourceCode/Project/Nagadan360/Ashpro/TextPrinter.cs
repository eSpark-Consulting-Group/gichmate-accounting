using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ashpro
{
	public class TextPrinter
	{
		[StructLayout(LayoutKind.Sequential)]
		public class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDocName;

			[MarshalAs(UnmanagedType.LPStr)]
			public string pOutputFile;

			[MarshalAs(UnmanagedType.LPStr)]
			public string pDataType;
		}

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "OpenPrinterA", ExactSpelling = true, SetLastError = true)]
		public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "StartDocPrinterA", ExactSpelling = true, SetLastError = true)]
		public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In][MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

		public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
		{
			int dwWritten = 0;
			IntPtr hPrinter = new IntPtr(0);
			DOCINFOA dOCINFOA = new DOCINFOA();
			bool flag = false;
			dOCINFOA.pDocName = "My C#.NET RAW Document";
			dOCINFOA.pDataType = "RAW";
			if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
			{
				if (StartDocPrinter(hPrinter, 1, dOCINFOA))
				{
					if (StartPagePrinter(hPrinter))
					{
						flag = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
						EndPagePrinter(hPrinter);
					}
					EndDocPrinter(hPrinter);
				}
				ClosePrinter(hPrinter);
			}
			if (!flag)
			{
				Marshal.GetLastWin32Error();
			}
			return flag;
		}

		public static bool SendFileToPrinter(string szPrinterName, string szFileName)
		{
			FileStream fileStream = new FileStream(szFileName, FileMode.Open);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			byte[] array = new byte[fileStream.Length];
			bool flag = false;
			IntPtr intPtr = new IntPtr(0);
			int num = Convert.ToInt32(fileStream.Length);
			array = binaryReader.ReadBytes(num);
			intPtr = Marshal.AllocCoTaskMem(num);
			Marshal.Copy(array, 0, intPtr, num);
			flag = SendBytesToPrinter(szPrinterName, intPtr, num);
			Marshal.FreeCoTaskMem(intPtr);
			return flag;
		}

		public static bool SendStringToPrinter(string szPrinterName, string szString)
		{
			int length = szString.Length;
			IntPtr intPtr = Marshal.StringToCoTaskMemAnsi(szString);
			SendBytesToPrinter(szPrinterName, intPtr, length);
			Marshal.FreeCoTaskMem(intPtr);
			return true;
		}
	}
}
