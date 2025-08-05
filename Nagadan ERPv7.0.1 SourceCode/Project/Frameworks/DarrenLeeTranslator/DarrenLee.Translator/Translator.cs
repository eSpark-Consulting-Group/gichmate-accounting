using System.IO;
using DarrenLee.Translator.TranslatorService;

namespace DarrenLee.Translator
{
	public class Translator
	{
		public static string Translate(string text, string fromLang, string toLang)
		{
			string text2 = null;
			try
			{
				LanguageServiceClient languageServiceClient = new LanguageServiceClient();
				return languageServiceClient.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", text, fromLang, toLang);
			}
			catch (IOException)
			{
				return "Please connect to the Internet!";
			}
		}
	}
}
