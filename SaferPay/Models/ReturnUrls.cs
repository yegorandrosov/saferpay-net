using System;

namespace SaferPay.Models
{
	public class ReturnUrls
	{
		public Uri Success { get; set; }
		public Uri Fail { get; set; }
		public Uri Abort { get; set; }
	}

	public class ReturnUrl
	{
		public Uri Url { get; set; }
	}
}