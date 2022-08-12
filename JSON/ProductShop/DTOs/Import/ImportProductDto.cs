namespace ProductShop.Dtos.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    using Newtonsoft.Json;

    [JsonObject]
    public class ImportProductDto
    {
        //"Name": "Care One Hemorrhoidal",
        //"Price": 932.18,
        //"SellerId": 25,
        //"BuyerId": 24

        [JsonPropertyName(nameof(Name))]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [JsonPropertyName(nameof(Price))]
        [Required]
        public decimal Price { get; set; }

        [JsonPropertyName(nameof(SellerId))]
        public int SellerId { get; set; }

        [JsonPropertyName(nameof(BuyerId))]
        public int? BuyerId { get; set; }


    }
}