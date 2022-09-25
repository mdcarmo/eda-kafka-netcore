using System.Text.Json.Serialization;

namespace ExKafka.Consumer.Console
{
    public class Order
    {
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{this.Quantity} ordens emitidas para o ativo {this.Symbol}, com valor de {this.Price}";
        }
    }
}