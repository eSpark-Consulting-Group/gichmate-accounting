namespace AccountsLibrary
{
	public class ExProductPOS
	{
		public int? ProductId { get; set; }

		public string ProductName { get; set; }

		public byte[] image { get; set; }

		public string Barcode { get; set; }

		public decimal Rate { get; set; }
	}
}
