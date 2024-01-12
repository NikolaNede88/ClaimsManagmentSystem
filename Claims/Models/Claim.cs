using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Claims.Models
{
    public class Claim
    {
    #region Properties

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "coverId")]
        public string CoverId { get; set; }

        [JsonProperty(PropertyName = "created")]

        [Range(typeof(DateTime), "1/1/2022", "1/3/2022",ErrorMessage = "Invalid Date")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "claimType")]
        public ClaimType Type { get; set; }

        [JsonProperty(PropertyName = "damageCost")]
        [Range(1, 100000, ErrorMessage = "Price must be between $1 and $100000")]
        public decimal DamageCost { get; set; }

    }

    #endregion

    #region Enums

    public enum ClaimType
    {
        Collision = 0,
        Grounding = 1,
        BadWeather = 2,
        Fire = 3
    }

    #endregion
}
