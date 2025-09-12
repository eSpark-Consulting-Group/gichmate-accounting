namespace AccountsLibrary
{
	public class DotmatrixMaster
	{
		public int? printMasterId { get; set; }

		public string DesignName { get; set; }

		public string VochrType { get; set; }

		public string FormType { get; set; }

		public bool isDefault { get; set; }

		public bool isLandScape { get; set; }

		public int? PageWidth { get; set; }

		public int? PageHieght { get; set; }

		public int? LinesPerPage { get; set; }

		public int? SpaceBtwnLines { get; set; }

		public string BackgroundImgPath { get; set; }

		public string PrinterName { get; set; }

		public string PrintType { get; set; }

		public int? upScroll { get; set; }

		public int? downScroll { get; set; }

		public string Riverse { get; set; }

		public string Bold_On { get; set; }

		public string Bold_Off { get; set; }

		public string Pitch { get; set; }

		public int? SpaceInLines { get; set; }
	}
}
