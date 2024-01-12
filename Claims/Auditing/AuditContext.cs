using Claims.Models;
using Microsoft.EntityFrameworkCore;

namespace Claims.Auditing
{
    public class AuditContext : DbContext
    {
        #region Constructor

        public AuditContext(DbContextOptions<AuditContext> options) : base(options)
        {
        }

        #endregion

        #region Methods

        public DbSet<ClaimAudit> ClaimAudits { get; set; }
        public DbSet<CoverAudit> CoverAudits { get; set; }

        #endregion
    }
}
