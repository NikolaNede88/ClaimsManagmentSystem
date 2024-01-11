namespace Claims.Models
{
    public class CoverAudit
    {
        #region Properties

        public int Id { get; set; }

        public string? CoverId { get; set; }

        public DateTime Created { get; set; }

        public string? HttpRequestType { get; set; }

        #endregion
    }
}
