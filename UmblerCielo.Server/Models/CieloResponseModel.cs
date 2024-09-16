using System.Runtime.CompilerServices;

namespace UmblerCielo.Server.Models
{
    // Modelo que representa a resposta da API da Cielo
    public class CieloResponseModel
    {
        // Propriedade principal da resposta, que contém as informações de pagamento
        public PaymentInfo? Payment { get; set; }

        // Classe interna que representa as informações de pagamento
        public class PaymentInfo
        {
            // ID único da transação (gerado pela API da Cielo)
            public string? PaymentId { get; set; }

            // TID (Transaction ID) gerado pela Cielo para identificação da transação
            public string? Tid { get; set; }

            // Código de autorização, caso a transação tenha sido autorizada com sucesso
            public string? AuthorizationCode { get; set; }

            // Valor da transação em centavos (ou seja, 100 = 1,00 real)
            public int? Amount { get; set; }

            // Status da transação (ex: "Autorizada", "Capturada", etc.)
            public string? Status { get; set; }

            // Informações sobre o cartão de crédito utilizado
            public CreditCardInfo? CreditCard { get; set; }
        }

        // Classe interna que representa os detalhes do cartão de crédito
        public class CreditCardInfo
        {
            // Número do cartão (normalmente mascarado na resposta)
            public string? CardNumber { get; set; }

            // Nome do titular do cartão
            public string? Holder { get; set; }

            // Data de expiração do cartão no formato MM/AA
            public string? ExpirationDate { get; set; }

            // Bandeira do cartão (ex: Visa, Mastercard, etc.)
            public string? Brand { get; set; }
        }
    }
}
