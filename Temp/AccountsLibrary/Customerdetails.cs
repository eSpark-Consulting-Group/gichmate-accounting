using System;

namespace AccountsLibrary
{
	public class Customerdetails
	{
		public Guid Id { get; set; }

		public string licenseKey { get; set; }

		public string macId { get; set; }

		public string ipAddress { get; set; }

		public string licenseType { get; set; }

		public string startDate { get; set; }

		public string expiryDate { get; set; }

		public string ForDays { get; set; }
	}
	public class CustomerDetails
	{
		public string EmailId { get; set; }

		public string Country { get; set; }

		public string IPAddress { get; set; }

		public string ActivationCode { get; set; }

		public string OTP { get; set; }

		public string Status { get; set; }

		public string Message { get; set; }

		public DateTime ActivationDate { get; set; }

		public DateTime ExpairingDate { get; set; }

		public bool isActive { get; set; }
	}
}
