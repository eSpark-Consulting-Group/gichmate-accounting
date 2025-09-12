namespace AccountsLibrary
{
	public class BarcodeLabelScaleEntity
	{
		public int ID { get; set; }

		public int ItemCode { get; set; }

		public int DepartmentID { get; set; }

		public int? GroupID { get; set; }

		public string Name1 { get; set; }

		public string Name2 { get; set; }

		public string Name3 { get; set; }

		public decimal Price { get; set; }

		public int? UnitID { get; set; }

		public int BarcodeType1 { get; set; }

		public int BarcodeType2 { get; set; }

		public int? Label1ID { get; set; }

		public int? Label2ID { get; set; }

		public string ProducedDate { get; set; }

		public string FreshnessDate { get; set; }

		public string ValidDate { get; set; }

		public string PackageType { get; set; }

		public string PackageWeight { get; set; }

		public string PackagePrice { get; set; }

		public string PackageRange { get; set; }

		public int? PackageDays { get; set; }

		public int? PackageHours { get; set; }

		public int? Text1ID { get; set; }

		public int? Text2ID { get; set; }

		public int? Text3ID { get; set; }

		public int? Text4ID { get; set; }

		public int? Text5ID { get; set; }

		public int? Text6ID { get; set; }

		public int? Text7ID { get; set; }

		public int? Text8ID { get; set; }

		public int? DiscountID { get; set; }

		public int DiscountRate { get; set; }

		public int HalfDiscount { get; set; }

		public int QuarterDiscount { get; set; }

		public int? TareID { get; set; }

		public int TareValue { get; set; }

		public int? IceID { get; set; }

		public int ICEValue { get; set; }

		public int? OriginID { get; set; }

		public int? TraceabilityID { get; set; }

		public int LimitPrice { get; set; }

		public int Tax1 { get; set; }

		public int Tax2 { get; set; }

		public int Tax3 { get; set; }

		public int Tax4 { get; set; }

		public int? Flag1 { get; set; }

		public int? Flag2 { get; set; }

		public int? Flag3 { get; set; }

		public string ProducedDateRule { get; set; }

		public string FreshnessDateFrom { get; set; }

		public string ValidDateFrom { get; set; }

		public string PackageDateFrom { get; set; }

		public string SpeedCode { get; set; }

		public string Position1 { get; set; }

		public string SalesCategory { get; set; }

		public string DiscountBeginTime { get; set; }

		public string DiscountEndTime { get; set; }

		public int? DiscountPrice { get; set; }

		public string DiscountFlag { get; set; }
	}
}
