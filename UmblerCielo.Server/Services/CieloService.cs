using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UmblerCielo.Server.Models;

namespace UmblerCielo.Server.Services
{
    public class CieloService
    {
        private readonly HttpClient _httpClient;

        public CieloService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CriarTransacao(CieloRequestModel request)
        {
            try
            {
                // Montar a URL da API da Cielo para criar transa��es (sandbox ou produ��o)
                string url = "https://apisandbox.cieloecommerce.cielo.com.br/1/sales/";

                // Serializar o request para JSON
                var requestContent = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json");

                // Configurar os cabe�alhos da requisi��o
                _httpClient.DefaultRequestHeaders.Clear(); // Limpa cabe�alhos anteriores
                _httpClient.DefaultRequestHeaders.Add("MerchantId", "fa752fab-a5dd-41f7-8b92-c9a234a0e35d"); // Substitua pelo seu MerchantId
                _httpClient.DefaultRequestHeaders.Add("MerchantKey", "YQYHMVNXSRLKSHRKSVRHAWJRXRDCPHXLRXZSMLQX"); // Substitua pela sua MerchantKey

                // Realizar a requisi��o POST � API da Cielo
                var response = await _httpClient.PostAsync(url, requestContent);

                // Verificar se a requisi��o foi bem-sucedida
                if (!response.IsSuccessStatusCode)
                {
                    // Captura a resposta de erro da API
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao criar transa��o: {response.StatusCode} - {errorResponse}");
                }

                // Deserializa a resposta da API
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Extrai o paymentId da resposta
                var responseObject = JsonSerializer.Deserialize<CieloResponseModel>(jsonResponse);

                // Verificar se o responseObject e Payment s�o nulos
                if (responseObject == null || responseObject.Payment == null)
                {
                    throw new InvalidOperationException("Resposta inv�lida da API Cielo.");
                }

                return responseObject.Payment.PaymentId ?? throw new InvalidOperationException("PaymentId n�o est� presente na resposta.");
            }
            catch (HttpRequestException httpEx)
            {
                // Trata erros de comunica��o HTTP, como problemas de rede
                Console.WriteLine($"Erro de rede ao chamar a API: {httpEx.Message}");
                throw new Exception("Erro ao conectar com a API da Cielo.");
            }
            catch (Exception ex)
            {
                // Trata qualquer outro tipo de erro
                Console.WriteLine($"Erro durante a cria��o da transa��o: {ex.Message}");
                throw new Exception("Erro ao processar a transa��o.");
            }
        }

        public async Task<string> CapturarTransacao(string paymentId, double amount)
        {
            try
            {
                // URL da API para capturar uma transa��o
                string url = $"https://apisandbox.cieloecommerce.cielo.com.br/1/sales/{paymentId}/capture";

                // Cria o corpo da requisi��o com o valor da captura
                var captureRequest = new
                {
                    Amount = (int)(amount * 100) // Converte o valor para centavos
                };

                // Serializa o corpo da requisi��o para JSON
                var requestContent = new StringContent(
                    JsonSerializer.Serialize(captureRequest),
                    Encoding.UTF8,
                    "application/json");

                // Configurar os cabe�alhos da requisi��o
                _httpClient.DefaultRequestHeaders.Clear(); // Limpa cabe�alhos anteriores
                _httpClient.DefaultRequestHeaders.Add("MerchantId", "fa752fab-a5dd-41f7-8b92-c9a234a0e35d"); // Substitua pelo seu MerchantId
                _httpClient.DefaultRequestHeaders.Add("MerchantKey", "YQYHMVNXSRLKSHRKSVRHAWJRXRDCPHXLRXZSMLQX"); // Substitua pela sua MerchantKey

                // Realiza a requisi��o PUT � API da Cielo
                var response = await _httpClient.PutAsync(url, requestContent);

                // Verifica se a requisi��o foi bem-sucedida
                if (!response.IsSuccessStatusCode)
                {
                    // Captura a resposta de erro da API
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao capturar transa��o: {response.StatusCode} - {errorResponse}");
                }

                // Retorna a resposta como string
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException httpEx)
            {
                // Trata erros de comunica��o HTTP, como problemas de rede
                Console.WriteLine($"Erro de rede ao chamar a API: {httpEx.Message}");
                throw new Exception("Erro ao conectar com a API da Cielo.");
            }
            catch (Exception ex)
            {
                // Trata qualquer outro tipo de erro
                Console.WriteLine($"Erro durante a captura da transa��o: {ex.Message}");
                throw new Exception("Erro ao processar a captura da transa��o.");
            }
        }

        public async Task<string> CancelarTransacao(string paymentId)
        {
            try
            {
                // URL da API para cancelar uma transa��o
                string url = $"https://apisandbox.cieloecommerce.cielo.com.br/1/sales/{paymentId}/void";

                // Configurar os cabe�alhos da requisi��o
                _httpClient.DefaultRequestHeaders.Clear(); // Limpa cabe�alhos anteriores
                _httpClient.DefaultRequestHeaders.Add("MerchantId", "fa752fab-a5dd-41f7-8b92-c9a234a0e35d"); // Substitua pelo seu MerchantId
                _httpClient.DefaultRequestHeaders.Add("MerchantKey", "YQYHMVNXSRLKSHRKSVRHAWJRXRDCPHXLRXZSMLQX"); // Substitua pela sua MerchantKey

                // Realiza a requisi��o PUT � API da Cielo
                var response = await _httpClient.PutAsync(url, null);

                // Verifica se a requisi��o foi bem-sucedida
                response.EnsureSuccessStatusCode();

                // Retorna a resposta como string
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException httpEx)
            {
                // Trata erros de comunica��o HTTP, como problemas de rede
                Console.WriteLine($"Erro de rede ao chamar a API: {httpEx.Message}");
                throw new Exception("Erro ao conectar com a API da Cielo.");
            }
            catch (Exception ex)
            {
                // Trata qualquer outro tipo de erro
                Console.WriteLine($"Erro durante o cancelamento da transa��o: {ex.Message}");
                throw new Exception("Erro ao processar o cancelamento da transa��o.");
            }
        }
    }
}
