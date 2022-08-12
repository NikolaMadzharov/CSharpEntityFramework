namespace ProductShop.Dtos.Export
{
    using System.Text.Json.Serialization;

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportSoldProductDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("buyerFirstName")]
        public string BuyerFirstName { get; set; }

        [JsonPropertyName("buyerLastName")]
        public string BuyerLastName { get; set; }

        [JsonProperty("soldProducts")]
        public ExportSoldProductDto[] SoldProducts { get; set; }



    }
}