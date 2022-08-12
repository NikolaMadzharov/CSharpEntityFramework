namespace ProductShop.Dtos.Import
{

    using System.ComponentModel.DataAnnotations;

    using System.Text.Json.Serialization;
    using Newtonsoft.Json;

    [JsonObject]
    public class ImportCategoryDto
    {
        //"Name": "Agaricus Equisetum Special Order",
        //"Price": 585.93,
        //"SellerId": 22,
        //"BuyerId": 27
        [JsonPropertyName(nameof(Name))]
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        [JsonPropertyName(nameof(Price))]
        public decimal Price { get; set; }

        [JsonPropertyName(nameof(SellerId))]
        public int SellerId { get; set; }

        [JsonPropertyName(nameof(BuyerId))]
        public int BuyerId { get; set; }
        

    }
}