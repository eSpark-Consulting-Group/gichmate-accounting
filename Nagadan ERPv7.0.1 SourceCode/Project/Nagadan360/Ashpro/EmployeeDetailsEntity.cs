namespace Ashpro
{
	public class EmployeeDetailsEntity
	{
		public int? EmpID { get; set; }

		public int? EmployeeID { get; set; }

		public string EmpName { get; set; }

		public string AddressOne { get; set; }

		public string AddressTwo { get; set; }

		public string AddressThree { get; set; }

		public string PhNumber { get; set; }

		public string EmailId { get; set; }

		public string Designation { get; set; }

		public decimal Salary { get; set; }

		public decimal CasualLeave { get; set; }

		public string Gender { get; set; }

		public string Qualification { get; set; }

		public string ParentName { get; set; }

		public string Relation { get; set; }

		public string ParentAddress { get; set; }

		public string ParentPhNo { get; set; }

		public string Remarks { get; set; }

		public bool isActive { get; set; }

		public string AdharNo { get; set; }

		public int? SalaryAccntId { get; set; }

		public int? SalaryAdvAccntId { get; set; }

		public int? SalaryPayableId { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
