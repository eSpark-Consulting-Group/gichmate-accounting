using System;

namespace Ashpro
{
	public class ProductBarcodeEntity
	{
		public int? Id { get; set; }

		public string Barcode { get; set; }

		public string BarcodeType { get; set; }

		public DateTime? ExpairyDate { get; set; }

		public DateTime? ManufDate { get; set; }

		public int? FK_ProductId { get; set; }

		public int? FK_UnitId { get; set; }

		public int? FK_BatchId { get; set; }

		public int? FK_BranchId { get; set; }

		public string Remarks { get; set; }

		public decimal PurchasePrice { get; set; }

		public decimal SellingPrice { get; set; }

		public decimal MRP { get; set; }

		public decimal ProductCost { get; set; }

		public string FieldOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }

		public decimal MinimuPrice { get; set; }

		public decimal RetailPrice { get; set; }

		public decimal WholesalePrice { get; set; }

		public string ProductBarcodeName { get; set; }

		public string LocalName { get; set; }

		public byte[] image { get; set; }
	}
}
