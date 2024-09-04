namespace SaferPay.Models
{
	public class SaferPaySettings
	{
		public ESaferPayEnvironment Environment { get; set; }
		
		public string Username { get; set; }

		public string Password { get; set; }

		public string CustomerId { get; set; }

		public string TerminalId { get; set; }
	}
}