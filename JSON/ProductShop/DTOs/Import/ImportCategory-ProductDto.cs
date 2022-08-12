namespace ProductShop.Dtos.Import
{
    using System.Text.Json.Serialization;

    using Newtonsoft.Json;

    [JsonObject]
    public class ImportCategory_ProductDto
    {
        //"CategoryId": 4,
        //"ProductId": 1

        [JsonPropertyName(nameof(CategoryId))]
        public int CategoryId { get; set; }

        [JsonPropertyName(nameof(ProductId))]
        public int ProductId { get; set; }
    }
}