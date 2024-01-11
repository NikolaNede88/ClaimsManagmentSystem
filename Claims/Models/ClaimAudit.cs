namespace Claims.Models
{
    public class ClaimAudit
    {
        #region Properties

        public int Id { get; set; }

        public string? ClaimId { get; set; }

        public DateTime Created { get; set; }

        public string? HttpRequestType { get; set; }

        #endregion
    }
}
