namespace AccountsLibrary
{
	public class BarcodeHDR
	{
		public int? Id { get; set; }

		public string name { get; set; }

		public int FirstMargin { get; set; }

		public int TopMargin { get; set; }

		public int RightMargin { get; set; }

		public int BottomMargin { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public int Accross { get; set; }

		public int Down { get; set; }

		public int TotalPerPage { get; set; }

		public string PrinterName { get; set; }

		public string PaperName { get; set; }

		public int PSWidth { get; set; }

		public int PSHieght { get; set; }

		public bool isBorderIncluded { get; set; }

		public bool isCuttingEdge { get; set; }

		public int BarcodeField { get; set; }

		public bool isBarcode { get; set; }
	}
}
