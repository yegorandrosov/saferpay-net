using System;

namespace SaferPay.Models
{
	public class Transaction
	{
		public string Type { get; set; }
		public string Status { get; set; }
		public string Id { get; set; }
		public DateTimeOffset Date { get; set; }
		public Amount Amount { get; set; }
    }
}