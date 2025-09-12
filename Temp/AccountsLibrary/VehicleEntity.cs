using System;

namespace AccountsLibrary
{
	public class VehicleEntity
	{
		public int? VanId { get; set; }

		public string VehName { get; set; }

		public string Manufacturer { get; set; }

		public string Model { get; set; }

		public string YearOfModel { get; set; }

		public string RegNo { get; set; }

		public string Ownership { get; set; }

		public string RegisterdOwner { get; set; }

		public string EngineNo { get; set; }

		public string ChasisNo { get; set; }

		public DateTime? InsurenceDate { get; set; }

		public DateTime? TaxDate { get; set; }

		public DateTime? PolusionDate { get; set; }

		public string BColor { get; set; }

		public DateTime? LoanEMIDate { get; set; }

		public string LoanEMIAmount { get; set; }

		public string Remarks { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
