namespace AccountsLibrary
{
	public class CurrencyInfo
	{
		public int? CurrencyID { get; set; }

		public string CurrencyCode { get; set; }

		public bool IsCurrencyNameFeminine { get; set; }

		public string EnglishCurrencyName { get; set; }

		public string EnglishPluralCurrencyName { get; set; }

		public string EnglishCurrencyPartName { get; set; }

		public string EnglishPluralCurrencyPartName { get; set; }

		public string Arabic1CurrencyName { get; set; }

		public string Arabic2CurrencyName { get; set; }

		public string Arabic310CurrencyName { get; set; }

		public string Arabic1199CurrencyName { get; set; }

		public string Arabic1CurrencyPartName { get; set; }

		public string Arabic2CurrencyPartName { get; set; }

		public string Arabic310CurrencyPartName { get; set; }

		public string Arabic1199CurrencyPartName { get; set; }

		public int PartPrecision { get; set; }

		public bool IsCurrencyPartNameFeminine { get; set; }
	}
}
