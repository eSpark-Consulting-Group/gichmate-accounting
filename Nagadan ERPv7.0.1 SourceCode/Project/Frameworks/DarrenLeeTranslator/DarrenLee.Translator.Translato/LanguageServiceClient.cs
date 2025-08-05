using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace DarrenLee.Translator.TranslatorService
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class LanguageServiceClient : ClientBase<LanguageService>, LanguageService
	{
		public LanguageServiceClient()
		{
		}

		public LanguageServiceClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public LanguageServiceClient(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public LanguageServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public LanguageServiceClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		public string[] GetLanguages(string appId)
		{
			return base.Channel.GetLanguages(appId);
		}

		public Task<string[]> GetLanguagesAsync(string appId)
		{
			return base.Channel.GetLanguagesAsync(appId);
		}

		public string[] GetLanguageNames(string appId, string locale)
		{
			return base.Channel.GetLanguageNames(appId, locale);
		}

		public Task<string[]> GetLanguageNamesAsync(string appId, string locale)
		{
			return base.Channel.GetLanguageNamesAsync(appId, locale);
		}

		public string Detect(string appId, string text)
		{
			return base.Channel.Detect(appId, text);
		}

		public Task<string> DetectAsync(string appId, string text)
		{
			return base.Channel.DetectAsync(appId, text);
		}

		public string Translate(string appId, string text, string from, string to)
		{
			return base.Channel.Translate(appId, text, from, to);
		}

		public Task<string> TranslateAsync(string appId, string text, string from, string to)
		{
			return base.Channel.TranslateAsync(appId, text, from, to);
		}
	}
}
