using System.Collections.Generic;

namespace AccountsLibrary
{
	public class BarcodeFormatClass
	{
		public BarcodeHDR _barcodeHDR { get; set; }

		public List<BarcodeDTL> _barcodeDTLs { get; set; }
	}
}
