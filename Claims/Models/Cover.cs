using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Claims.Models;

public class Cover
{
 #region Properties

    [JsonProperty(PropertyName = "id")]
    public required string Id { get; set; }

    [JsonProperty(PropertyName = "startDate")]
   //in this version of C# date validation is not supported
    public DateOnly StartDate { get; set; }

    [JsonProperty(PropertyName = "endDate")]
    public DateOnly EndDate { get; set; }

    [JsonProperty(PropertyName = "claimType")]
    public CoverType Type { get; set; }

    [JsonProperty(PropertyName = "premium")]
    public decimal Premium { get; set; }
}

#endregion

 #region Enums

public enum CoverType
{
    Yacht = 0,
    PassengerShip = 1,
    ContainerShip = 2,
    BulkCarrier = 3,
    Tanker = 4
}

#endregion
