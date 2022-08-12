namespace ProductShop.Dtos.Export
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportProductDto
    {

        [JsonPropertyName("name")]
        
        public string Name { get; set; }

        [JsonPropertyName("price")]
     
        public decimal Price { get; set; }


        [JsonPropertyName("seller")]
        public string SellerFullname { get; set; }

    }
}