using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AccountsLibrary.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string strSQLCon
		{
			get
			{
				return (string)this["strSQLCon"];
			}
			set
			{
				this["strSQLCon"] = value;
			}
		}

		[DefaultSettingValue(" .\\SQLEXPRESS")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public string strServer
		{
			get
			{
				return (string)this["strServer"];
			}
			set
			{
				this["strServer"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		public int intLogOnId
		{
			get
			{
				return (int)this["intLogOnId"];
			}
			set
			{
				this["intLogOnId"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool isServer
		{
			get
			{
				return (bool)this["isServer"];
			}
			set
			{
				this["isServer"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string strConnetionString
		{
			get
			{
                return ConfigurationManager.ConnectionStrings["PSVAT5"]?.ConnectionString; // App.config has connection string with database!
                //return (string)this["strConnetionString"];
			}
			set
			{
				this["strConnetionString"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("sa")]
		[UserScopedSetting]
		public string sUserName
		{
			get
			{
				return (string)this["sUserName"];
			}
			set
			{
				this["sUserName"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("at123")]
		[UserScopedSetting]
		public string sPassword
		{
			get
			{
				return (string)this["sPassword"];
			}
			set
			{
				this["sPassword"] = value;
			}
		}

		[DefaultSettingValue("")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public string strBackImageName
		{
			get
			{
				return (string)this["strBackImageName"];
			}
			set
			{
				this["strBackImageName"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool isMain
		{
			get
			{
				return (bool)this["isMain"];
			}
			set
			{
				this["isMain"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("en")]
		public string Language
		{
			get
			{
				return (string)this["Language"];
			}
			set
			{
				this["Language"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string MainForm
		{
			get
			{
				return (string)this["MainForm"];
			}
			set
			{
				this["MainForm"] = value;
			}
		}

		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool isPrintOn
		{
			get
			{
				return (bool)this["isPrintOn"];
			}
			set
			{
				this["isPrintOn"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		public bool UnitBarcodeAuto
		{
			get
			{
				return (bool)this["UnitBarcodeAuto"];
			}
			set
			{
				this["UnitBarcodeAuto"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("1")]
		[UserScopedSetting]
		public int iBranchId
		{
			get
			{
				return (int)this["iBranchId"];
			}
			set
			{
				this["iBranchId"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		public DateTime appDate
		{
			get
			{
				return (DateTime)this["appDate"];
			}
			set
			{
				this["appDate"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		public bool IsActivated
		{
			get
			{
				return (bool)this["IsActivated"];
			}
			set
			{
				this["IsActivated"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string TerminalPrinter
		{
			get
			{
				return (string)this["TerminalPrinter"];
			}
			set
			{
				this["TerminalPrinter"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("COM3")]
		[UserScopedSetting]
		public string sCom
		{
			get
			{
				return (string)this["sCom"];
			}
			set
			{
				this["sCom"] = value;
			}
		}

		[DefaultSettingValue("Teal")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color BackColor
		{
			get
			{
				return (Color)this["BackColor"];
			}
			set
			{
				this["BackColor"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("White")]
		[UserScopedSetting]
		public Color ForeColor
		{
			get
			{
				return (Color)this["ForeColor"];
			}
			set
			{
				this["ForeColor"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string BackupPath
		{
			get
			{
				return (string)this["BackupPath"];
			}
			set
			{
				this["BackupPath"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string RDLCPrinter
		{
			get
			{
				return (string)this["RDLCPrinter"];
			}
			set
			{
				this["RDLCPrinter"] = value;
			}
		}

		[DefaultSettingValue("")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public string CDPrinter
		{
			get
			{
				return (string)this["CDPrinter"];
			}
			set
			{
				this["CDPrinter"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string MiniReportPrinter
		{
			get
			{
				return (string)this["MiniReportPrinter"];
			}
			set
			{
				this["MiniReportPrinter"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string CounterName
		{
			get
			{
				return (string)this["CounterName"];
			}
			set
			{
				this["CounterName"] = value;
			}
		}

		[DefaultSettingValue("128, 255, 255")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color NumberBB
		{
			get
			{
				return (Color)this["NumberBB"];
			}
			set
			{
				this["NumberBB"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color NumberBF
		{
			get
			{
				return (Color)this["NumberBF"];
			}
			set
			{
				this["NumberBF"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("128, 255, 255")]
		public Color CategoryBB
		{
			get
			{
				return (Color)this["CategoryBB"];
			}
			set
			{
				this["CategoryBB"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public Color CategoryBF
		{
			get
			{
				return (Color)this["CategoryBF"];
			}
			set
			{
				this["CategoryBF"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("128, 255, 255")]
		[DebuggerNonUserCode]
		public Color ProductBB
		{
			get
			{
				return (Color)this["ProductBB"];
			}
			set
			{
				this["ProductBB"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color ProductBF
		{
			get
			{
				return (Color)this["ProductBF"];
			}
			set
			{
				this["ProductBF"] = value;
			}
		}

		[DefaultSettingValue("128, 255, 255")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color TopIBB
		{
			get
			{
				return (Color)this["TopIBB"];
			}
			set
			{
				this["TopIBB"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public Color TopIBF
		{
			get
			{
				return (Color)this["TopIBF"];
			}
			set
			{
				this["TopIBF"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("128, 255, 255")]
		public Color SideIBB
		{
			get
			{
				return (Color)this["SideIBB"];
			}
			set
			{
				this["SideIBB"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		public Color SideIBF
		{
			get
			{
				return (Color)this["SideIBF"];
			}
			set
			{
				this["SideIBF"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("128, 255, 255")]
		[UserScopedSetting]
		public Color GeneralBB
		{
			get
			{
				return (Color)this["GeneralBB"];
			}
			set
			{
				this["GeneralBB"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color GeneralBF
		{
			get
			{
				return (Color)this["GeneralBF"];
			}
			set
			{
				this["GeneralBF"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Black")]
		public Color TitlebarPB
		{
			get
			{
				return (Color)this["TitlebarPB"];
			}
			set
			{
				this["TitlebarPB"] = value;
			}
		}

		[DefaultSettingValue("White")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color TitlebarPF
		{
			get
			{
				return (Color)this["TitlebarPF"];
			}
			set
			{
				this["TitlebarPF"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("LightGray")]
		public Color DetailPB
		{
			get
			{
				return (Color)this["DetailPB"];
			}
			set
			{
				this["DetailPB"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Black")]
		public Color DetailPF
		{
			get
			{
				return (Color)this["DetailPF"];
			}
			set
			{
				this["DetailPF"] = value;
			}
		}

		[DefaultSettingValue("LightGray")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public Color NumberpadPB
		{
			get
			{
				return (Color)this["NumberpadPB"];
			}
			set
			{
				this["NumberpadPB"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("LightGray")]
		[UserScopedSetting]
		public Color CategoryPB
		{
			get
			{
				return (Color)this["CategoryPB"];
			}
			set
			{
				this["CategoryPB"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("LightGray")]
		public Color ProductPB
		{
			get
			{
				return (Color)this["ProductPB"];
			}
			set
			{
				this["ProductPB"] = value;
			}
		}

		[DefaultSettingValue("LightGray")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color TopPB
		{
			get
			{
				return (Color)this["TopPB"];
			}
			set
			{
				this["TopPB"] = value;
			}
		}

		[DefaultSettingValue("LightGray")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color SidePB
		{
			get
			{
				return (Color)this["SidePB"];
			}
			set
			{
				this["SidePB"] = value;
			}
		}

		[DefaultSettingValue("128, 255, 255")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color DgvheaderB
		{
			get
			{
				return (Color)this["DgvheaderB"];
			}
			set
			{
				this["DgvheaderB"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("Black")]
		public Color DgvheaderF
		{
			get
			{
				return (Color)this["DgvheaderF"];
			}
			set
			{
				this["DgvheaderF"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("LightGray")]
		[DebuggerNonUserCode]
		public Color DgvB
		{
			get
			{
				return (Color)this["DgvB"];
			}
			set
			{
				this["DgvB"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("128, 255, 255")]
		[DebuggerNonUserCode]
		public Color LoginB
		{
			get
			{
				return (Color)this["LoginB"];
			}
			set
			{
				this["LoginB"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		public Color LoginF
		{
			get
			{
				return (Color)this["LoginF"];
			}
			set
			{
				this["LoginF"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("128, 255, 255")]
		[DebuggerNonUserCode]
		public Color OtherB
		{
			get
			{
				return (Color)this["OtherB"];
			}
			set
			{
				this["OtherB"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color OtherF
		{
			get
			{
				return (Color)this["OtherF"];
			}
			set
			{
				this["OtherF"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		public bool IsSalesCounter
		{
			get
			{
				return (bool)this["IsSalesCounter"];
			}
			set
			{
				this["IsSalesCounter"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool IsCashClosed
		{
			get
			{
				return (bool)this["IsCashClosed"];
			}
			set
			{
				this["IsCashClosed"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public bool IsNumPanelShow
		{
			get
			{
				return (bool)this["IsNumPanelShow"];
			}
			set
			{
				this["IsNumPanelShow"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool IsTopPanelShow
		{
			get
			{
				return (bool)this["IsTopPanelShow"];
			}
			set
			{
				this["IsTopPanelShow"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("1")]
		public int iPrintCopy
		{
			get
			{
				return (int)this["iPrintCopy"];
			}
			set
			{
				this["iPrintCopy"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("56")]
		[UserScopedSetting]
		public int iProductsPerPage
		{
			get
			{
				return (int)this["iProductsPerPage"];
			}
			set
			{
				this["iProductsPerPage"] = value;
			}
		}

		[DefaultSettingValue("9")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public int iGroupsPerPage
		{
			get
			{
				return (int)this["iGroupsPerPage"];
			}
			set
			{
				this["iGroupsPerPage"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("1")]
		[DebuggerNonUserCode]
		public int iTockenNo
		{
			get
			{
				return (int)this["iTockenNo"];
			}
			set
			{
				this["iTockenNo"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string ScalePort
		{
			get
			{
				return (string)this["ScalePort"];
			}
			set
			{
				this["ScalePort"] = value;
			}
		}

		[DefaultSettingValue("")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public string CloseCashPDF
		{
			get
			{
				return (string)this["CloseCashPDF"];
			}
			set
			{
				this["CloseCashPDF"] = value;
			}
		}

		[DefaultSettingValue("")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public string strImageFolderSecondDisplay
		{
			get
			{
				return (string)this["strImageFolderSecondDisplay"];
			}
			set
			{
				this["strImageFolderSecondDisplay"] = value;
			}
		}

		[DefaultSettingValue("8000")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int IdleTime
		{
			get
			{
				return (int)this["IdleTime"];
			}
			set
			{
				this["IdleTime"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		public Color CompanyBColor
		{
			get
			{
				return (Color)this["CompanyBColor"];
			}
			set
			{
				this["CompanyBColor"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("White")]
		[UserScopedSetting]
		public Color CompanyFColor
		{
			get
			{
				return (Color)this["CompanyFColor"];
			}
			set
			{
				this["CompanyFColor"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		public Color ProImageBColor
		{
			get
			{
				return (Color)this["ProImageBColor"];
			}
			set
			{
				this["ProImageBColor"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color ProDetailsBColor
		{
			get
			{
				return (Color)this["ProDetailsBColor"];
			}
			set
			{
				this["ProDetailsBColor"] = value;
			}
		}

		[DefaultSettingValue("Black")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color TotalLabelBColor
		{
			get
			{
				return (Color)this["TotalLabelBColor"];
			}
			set
			{
				this["TotalLabelBColor"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string Activationlicense
		{
			get
			{
				return (string)this["Activationlicense"];
			}
			set
			{
				this["Activationlicense"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ActivationStartDate
		{
			get
			{
				return (string)this["ActivationStartDate"];
			}
			set
			{
				this["ActivationStartDate"] = value;
			}
		}

		[DefaultSettingValue("")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public string ActivationEndDate
		{
			get
			{
				return (string)this["ActivationEndDate"];
			}
			set
			{
				this["ActivationEndDate"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string ActivationMacID
		{
			get
			{
				return (string)this["ActivationMacID"];
			}
			set
			{
				this["ActivationMacID"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string ActivationKey
		{
			get
			{
				return (string)this["ActivationKey"];
			}
			set
			{
				this["ActivationKey"] = value;
			}
		}
	}
}
