namespace SaferPay.Models
{
	public class PaymentPageInitializeRequest : RequestBase
	{
		public string TerminalId { get; set; }
		public InitializationPayment Payment { get; set; }
		public InitializationPaymentMeans PaymentMeans { get; set; }
		public Payer Payer { get; set; }
		public ReturnUrl ReturnUrl { get; set; }
	}
}