using System.Runtime.CompilerServices;

namespace UmblerCielo.Server.Models
{
    // Modelo que representa a resposta da API da Cielo
    public class CieloResponseModel
    {
        // Propriedade principal da resposta, que cont�m as informa��es de pagamento
        public PaymentInfo? Payment { get; set; }

        // Classe interna que representa as informa��es de pagamento
        public class PaymentInfo
        {
            // ID �nico da transa��o (gerado pela API da Cielo)
            public string? PaymentId { get; set; }

            // TID (Transaction ID) gerado pela Cielo para identifica��o da transa��o
            public string? Tid { get; set; }

            // C�digo de autoriza��o, caso a transa��o tenha sido autorizada com sucesso
            public string? AuthorizationCode { get; set; }

            // Valor da transa��o em centavos (ou seja, 100 = 1,00 real)
            public int? Amount { get; set; }

            // Status da transa��o (ex: "Autorizada", "Capturada", etc.)
            public string? Status { get; set; }

            // Informa��es sobre o cart�o de cr�dito utilizado
            public CreditCardInfo? CreditCard { get; set; }
        }

        // Classe interna que representa os detalhes do cart�o de cr�dito
        public class CreditCardInfo
        {
            // N�mero do cart�o (normalmente mascarado na resposta)
            public string? CardNumber { get; set; }

            // Nome do titular do cart�o
            public string? Holder { get; set; }

            // Data de expira��o do cart�o no formato MM/AA
            public string? ExpirationDate { get; set; }

            // Bandeira do cart�o (ex: Visa, Mastercard, etc.)
            public string? Brand { get; set; }
        }
    }
}
