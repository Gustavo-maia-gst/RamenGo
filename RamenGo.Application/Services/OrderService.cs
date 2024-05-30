using RamenGo.Domain.Entities;
using RamenGo.Domain.Repositories;
using System.Text.Json;

namespace RamenGo.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProteinRepository _proteinRepository;
        private readonly IBrothRepository _brothRepository;
        private readonly HttpClient _httpClient;

        private const string GENERATE_ID_URL = "https://api.tech.redventures.com.br/orders/generate-id";
        private const string X_API_KEY = "ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf";

        public OrderService(IOrderRepository orderRepository,
                            IProteinRepository proteinRepository,
                            IBrothRepository brothRepository)
        {
            _orderRepository = orderRepository;
            _proteinRepository = proteinRepository;
            _brothRepository = brothRepository;
            _httpClient = new HttpClient();
        }

        public Order CreateOrder(int proteinId, int brothId)
        {
            int id = GetNewId().Result;

            Protein protein = _proteinRepository.Get(proteinId);
            Broth broth = _brothRepository.Get(brothId);

            Order order = new(id, broth, protein);
            _orderRepository.Insert(order);
            return order;
        }

        public Order GetOrder(int id) => _orderRepository.Get(id);

        private async Task<int> GetNewId()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, GENERATE_ID_URL);
            request.Headers.Add("x-api-key", X_API_KEY);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Dictionary<string, string>>(resultJson)!;
                return int.Parse(result["orderId"]);
            }

            throw new Exception("Cannot connect to the red ventures server");
        }
    }
}
