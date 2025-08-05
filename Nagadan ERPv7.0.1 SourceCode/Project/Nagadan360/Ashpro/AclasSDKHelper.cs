using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ashpro
{
	internal class AclasSDKHelper
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct TASSDKDeviceInfo
		{
			public uint ProtocolType;

			public uint Addr;

			public uint Port;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Name;

			public uint ID;

			public uint Version;

			public byte Country;

			public byte DepartmentID;

			public byte KeyType;

			public ulong PrinterDot;

			public long PrnStartDate;

			public uint LabelPage;

			public uint PrinterNo;

			public ushort PLUStorage;

			public ushort HotKeyCount;

			public ushort NutritionStorage;

			public ushort DiscountStorage;

			public ushort Note1Storage;

			public ushort Note2Storage;

			public ushort Note3Storage;

			public ushort Note4Storage;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
			public byte[] FirmwareVersion;

			public byte DefaultProtocol;

			public byte LFCodeLen;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] DeviceId;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] StockId;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 155)]
			public byte[] Adjunct;
		}

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void TASSDKOnProgressEvent(uint nErrorCode, uint Index, uint Total, IntPtr lpUserData);

		private const string LibraryName = "AclasSDK.dll";

		private const int ASSDK_Err_Success = 0;

		private const int ASSDK_Err_Progress = 1;

		private const int ASSDK_Err_Terminate = 2;

		private const int ASSDK_ProtocolType_None = 0;

		private const int ASSDK_ProtocolType_Pecr = 1;

		private const int ASSDK_ProtocolType_Hecr = 2;

		private const int ASSDK_ProtocolType_TSecr = 3;

		private const int ASSDK_ProcType_Down = 0;

		private const int ASSDK_ProcType_UP = 1;

		private const int ASSDK_ProcType_Edit = 2;

		private const int ASSDK_ProcType_Del = 3;

		private const int ASSDK_ProcType_List = 4;

		private const int ASSDK_ProcType_Empty = 5;

		private const int ASSDK_ProcType_Reserve = 16;

		private const int ASSDK_DataType_PLU = 0;

		private const int ASSDK_DataType_Unit = 1;

		private const int ASSDK_DataType_Department = 2;

		private const int ASSDK_DataType_HotKey = 3;

		private const int ASSDK_DataType_Group = 4;

		private const int ASSDK_DataType_Discount = 5;

		private const int ASSDK_DataType_Origin = 6;

		private const int ASSDK_DataType_Country = 7;

		private const int ASSDK_DataType_SlaughterHouse = 8;

		private const int ASSDK_DataType_Cuttinghall = 9;

		private const int ASSDK_DataType_Tare = 10;

		private const int ASSDK_DataType_Nutrition = 11;

		private const int ASSDK_DataType_Note1 = 12;

		private const int ASSDK_DataType_Note2 = 13;

		private const int ASSDK_DataType_Note3 = 14;

		private const int ASSDK_DataType_Options = 16;

		private const int ASSDK_DataType_CustomBarcode = 17;

		private const int ASSDK_DataType_LabelPrintRecord = 18;

		private const int ASSDK_DataType_HeaderInfo = 19;

		private const int ASSDK_DataType_FooterInfo = 20;

		private const int ASSDK_DataType_AdvertisementInfo = 21;

		private const int ASSDK_DataType_HeaderLogo = 22;

		private const int ASSDK_DataType_FooterLogo = 23;

		private const int ASSDK_DataType_LabelAdvertisement = 24;

		private const int ASSDK_DataType_VendorInfo = 25;

		private const int ASSDK_DataType_NutritionElement = 26;

		private const int ASSDK_DataType_NutritionInfo = 27;

		private const int ASSDK_DataType_Note4 = 28;

		[DllImport("AclasSDK.dll")]
		public static extern bool AclasSDK_Initialize(Pointer Adjuct = null);

		[DllImport("AclasSDK.dll")]
		public static extern void AclasSDK_Finalize();

		[DllImport("AclasSDK.dll", CharSet = CharSet.Ansi)]
		public static extern bool AclasSDK_GetDevicesInfo(uint Addr, uint Port, uint ProtocolType, ref TASSDKDeviceInfo DeviceInfo);

		[DllImport("AclasSDK.dll", CharSet = CharSet.Ansi)]
		public static extern int AclasSDK_GetNetworkSectionDevicesInfo(uint Addr, uint Port, uint ProtocolType, IntPtr lpDeviceInfos, uint dwCount);

		[DllImport("AclasSDK.dll")]
		public static extern IntPtr AclasSDK_ExecTaskA(uint Addr, uint Port, uint ProtocolType, uint ProcType, uint DataType, string FileName, TASSDKOnProgressEvent OnProgress, Pointer lpUserData);

		[DllImport("AclasSDK.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr AclasSDK_ExecTask(uint Addr, uint Port, uint ProtocolType, uint ProcType, uint DataType, string FileName, TASSDKOnProgressEvent OnProgress, Pointer lpUserData);

		[DllImport("AclasSDK.dll")]
		public static extern IntPtr AclasSDK_ExecTaskW(uint Addr, uint Port, uint ProtocolType, uint ProcType, uint DataType, string FileName, TASSDKOnProgressEvent OnProgress, Pointer lpUserData);

		[DllImport("AclasSDK.dll")]
		public static extern int AclasSDK_GetLastTaskError();

		[DllImport("AclasSDK.dll")]
		public static extern void AclasSDK_StopTask(IntPtr TaskHandle);

		[DllImport("AclasSDK.dll")]
		public static extern void AclasSDK_WaitForTask(IntPtr TaskHandle);

		public static uint MakeHostToDWord(string sHost)
		{
			uint num = 0u;
			string[] array = sHost.Split('.');
			if (array.Length != 4)
			{
				return num;
			}
			int num2 = 0;
			while (true)
			{
				if (num2 < array.Length)
				{
					Convert.ToUInt32(array[num2]);
					if (Convert.ToUInt32(array[num2]) > 255)
					{
						break;
					}
					num += Convert.ToUInt32(Convert.ToUInt32(array[num2]) << (3 - num2) * 8);
					num2++;
					continue;
				}
				return num;
			}
			return num;
		}

		public static void OnProgressEvent(uint nErrorCode, uint Index, uint Total, IntPtr lpUserData)
		{
			switch (nErrorCode)
			{
			case 0u:
				MessageBox.Show($"Complete, Total: {Total}");
				break;
			case 2u:
				MessageBox.Show("Proc Stop!");
				break;
			case 1u:
				break;
			}
		}

		public static void UploadDataToMachine(string sDataType, string sDeviceIp, string sFileName)
		{
			uint dataType = Convert.ToUInt32(sDataType);
			uint addr = MakeHostToDWord(sDeviceIp);
			TASSDKDeviceInfo DeviceInfo = default(TASSDKDeviceInfo);
			if (AclasSDK_GetDevicesInfo(addr, 0u, 0u, ref DeviceInfo))
			{
				TASSDKOnProgressEvent onProgress = OnProgressEvent;
				AclasSDK_WaitForTask(AclasSDK_ExecTaskA(DeviceInfo.Addr, DeviceInfo.Port, DeviceInfo.ProtocolType, 0u, dataType, sFileName, onProgress, null));
			}
		}

		public static void AClasInitialze()
		{
			AclasSDK_Initialize();
		}
	}
}
