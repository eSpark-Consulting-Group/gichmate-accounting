using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DarrenLee.Translator.TranslatorService
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://api.microsofttranslator.com/v1/soap.svc", ConfigurationName = "TranslatorService.LanguageService")]
	public interface LanguageService
	{
		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguages", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguagesResponse")]
		string[] GetLanguages(string appId);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguages", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguagesResponse")]
		Task<string[]> GetLanguagesAsync(string appId);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguageNames", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguageNamesResponse")]
		string[] GetLanguageNames(string appId, string locale);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguageNames", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/GetLanguageNamesResponse")]
		Task<string[]> GetLanguageNamesAsync(string appId, string locale);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/Detect", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/DetectResponse")]
		string Detect(string appId, string text);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/Detect", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/DetectResponse")]
		Task<string> DetectAsync(string appId, string text);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/Translate", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/TranslateResponse")]
		string Translate(string appId, string text, string from, string to);

		[OperationContract(Action = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/Translate", ReplyAction = "http://api.microsofttranslator.com/v1/soap.svc/LanguageService/TranslateResponse")]
		Task<string> TranslateAsync(string appId, string text, string from, string to);
	}
}
