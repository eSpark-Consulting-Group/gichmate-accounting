namespace Ashpro
{
	public class atReportParameter
	{
		public string ParameterName { get; set; }

		public string ParameterValue { get; set; }

		public atReportParameter()
		{
		}

		public atReportParameter(string _parameterName, string _parameterValue)
		{
			ParameterName = _parameterName;
			ParameterValue = _parameterValue;
		}
	}
}
