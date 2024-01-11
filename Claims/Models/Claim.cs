using Newtonsoft.Json;

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
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "claimType")]
        public ClaimType Type { get; set; }

        [JsonProperty(PropertyName = "damageCost")]
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
