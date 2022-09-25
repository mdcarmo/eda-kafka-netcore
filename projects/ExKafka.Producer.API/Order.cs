using System.Text.Json.Serialization;

namespace ExKafka.Producer.API
{
    public class Order
    {
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}