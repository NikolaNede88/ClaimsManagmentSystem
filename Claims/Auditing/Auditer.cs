using Claims.Models;

namespace Claims.Auditing
{
    public class Auditer : IAuditer
    {
        #region Properties 

        private readonly AuditContext _auditContext;

        #endregion

        #region Constructor

        public Auditer(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        #endregion

        #region Methods 

        public void AuditClaim(string id, string httpRequestType)
        {
            var claimAudit = new ClaimAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                ClaimId = id
            };

            _auditContext.Add(claimAudit);
            _auditContext.SaveChanges();
        }
        
        public void AuditCover(string id, string httpRequestType)
        {
            var coverAudit = new CoverAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                CoverId = id
            };

            _auditContext.Add(coverAudit);
            _auditContext.SaveChanges();
        }

        #endregion
    }
}
