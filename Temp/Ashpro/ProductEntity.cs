using System;

namespace Ashpro
{
	public class ProductEntity
	{
		public int? ProductId { get; set; }

		public int? ProductCode { get; set; }

		public string ProductName { get; set; }

		public string Specification { get; set; }

		public int? PGroupId { get; set; }

		public int? UnitId { get; set; }

		public int? BrandId { get; set; }

		public int? TCategoryId { get; set; }

		public string HSNNo { get; set; }

		public decimal PurchasePrice { get; set; }

		public decimal SellingPrice { get; set; }

		public decimal MRP { get; set; }

		public decimal OpeningStock { get; set; }

		public DateTime OppStockDate { get; set; }

		public decimal ReOrderStock { get; set; }

		public int? AccountId { get; set; }

		public int? GodwnId { get; set; }

		public int? SelfId { get; set; }

		public decimal PurchaseDis { get; set; }

		public decimal SaleDis { get; set; }

		public string AliasCode { get; set; }

		public string AliasName { get; set; }

		public string Remarks { get; set; }

		public string LocalName { get; set; }

		public int? PurchaseUnit { get; set; }

		public int? SalesUnit { get; set; }

		public int? ReportUnit { get; set; }

		public byte[] image { get; set; }

		public string filePath { get; set; }

		public string description { get; set; }

		public string ProductType { get; set; }

		public bool isActive { get; set; }

		public bool? isInclusive { get; set; }

		public string Barcode { get; set; }

		public bool? IsExpairing { get; set; }

		public decimal? ExpairingDays { get; set; }

		public decimal? MinimumMargin { get; set; }

		public decimal? NormalMargin { get; set; }

		public decimal? RetailMargin { get; set; }

		public decimal? WholesaleMargin { get; set; }

		public decimal? MRPMargin { get; set; }

		public bool? IsMarginBasedPercentage { get; set; }

		public int LoyalityPont { get; set; }

		public decimal? AgentCommision { get; set; }

		public decimal? SalesmanCommision { get; set; }

		public bool? IsCommisionBasedPercentage { get; set; }

		public string FieldOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }

		public string FieldFive { get; set; }

		public bool? IsScale { get; set; }

		public int ListOrder { get; set; }

		public bool? IsAllowMultiple { get; set; }

		public bool? isRateBasedWeight { get; set; }

		public string PriorityStatus { get; set; }

		public int? CookingTime { get; set; }

		public bool? IsFavorite { get; set; }

		public string RestaurantItemType { get; set; }
	}
}
