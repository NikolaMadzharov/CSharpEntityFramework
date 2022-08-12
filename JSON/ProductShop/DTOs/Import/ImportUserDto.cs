namespace ProductShop.Dtos.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    using Newtonsoft.Json;

    [JsonObject]
    public class ImportUserDto
    {
        [JsonPropertyName(nameof(firstName))]

        public string firstName { get; set; }

        [JsonPropertyName(nameof(lastName))]
        [Required]
        [MinLength(3)]
        public string lastName { get; set; }

        [JsonPropertyName(nameof(age))]
        public int? age { get; set; }
    }
}