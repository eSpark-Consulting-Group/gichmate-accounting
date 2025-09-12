namespace AccountsLibrary
{
	public class BarcodeDTL
	{
		public int? id { get; set; }

		public string Name { get; set; }

		public string Text { get; set; }

		public string Value { get; set; }

		public int Position_x { get; set; }

		public int Position_y { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string Alignment { get; set; }

		public string RotationPoint { get; set; }

		public string fontFamily { get; set; }

		public int fontSize { get; set; }

		public string fStyle { get; set; }

		public string Format { get; set; }

		public string bAlignment { get; set; }

		public int FK_BarcodeHDR { get; set; }

		public string Type { get; set; }
	}
}
