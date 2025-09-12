using System;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AccountsLibrary
{
	public class RegisterFont
	{
		[DllImport("gdi32", EntryPoint = "AddFontResource")]
		public static extern int AddFontResourceA(string lpFileName);

		[DllImport("gdi32.dll")]
		private static extern int AddFontResource(string lpszFilename);

		[DllImport("gdi32.dll")]
		private static extern int CreateScalableFontResource(uint fdwHidden, string lpszFontRes, string lpszFontFile, string lpszCurrentPath);

		public static void RegisterOwnFont(string contentFontName)
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), contentFontName);
			if (!File.Exists(text))
			{
				string sourceFileName = Application.StartupPath + "\\" + contentFontName;
				File.Copy(sourceFileName, text);
				PrivateFontCollection privateFontCollection = new PrivateFontCollection();
				privateFontCollection.AddFontFile(text);
				string name = privateFontCollection.Families[0].Name;
				AddFontResource(text);
				Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts", name, contentFontName, RegistryValueKind.String);
			}
		}
	}
}
