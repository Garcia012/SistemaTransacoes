namespace UmblerCielo.Server.Models
{
	public class CieloRequestModel
	{
		public string MerchantOrderId { get; set; } = string.Empty;
		

		public class Payment
		{
			public string? Type { get; set; } = "CreditCard";
			public int Amount { get; set; } // Valor em centavos
			public int Installments { get; set; }
			public CreditCard CreditCard { get; set; } = new CreditCard();
		}

		public class CreditCard
		{
			public string CardNumber { get; set; } = string.Empty;
			public string Holder { get; set; } = string.Empty;
			public string ExpirationDate { get; set; } = string.Empty;
			public string SecurityCode { get; set; } = string.Empty;
			public string Brand { get; set; } = string.Empty;// Visa ou Mastercard
		}
	}
}
