using Claims.Auditing;
using Claims.Models;
using Claims.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Claims.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        #region Properties

        private readonly ILogger<ClaimsController> _logger;
        private readonly ICosmoDbService _cosmosDbService;
        private readonly IAuditer _auditer;

        #endregion

        #region Constructor

        public ClaimsController(ILogger<ClaimsController> logger, ICosmoDbService cosmosDbService, AuditContext auditContext)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
            _auditer = new Auditer(auditContext);
        }

        #endregion

        #region Methods

        [HttpGet]
        public Task<IEnumerable<Claim>> GetAsync()
        {
            return _cosmosDbService.GetClaimsAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Claim claim)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.AddItemAsync(claim);
                _auditer.AuditClaim(claim.Id, "POST");
                return Ok(claim);
            }
            
            return BadRequest("Model is not valid");
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(string id)
        {
            _auditer.AuditClaim(id, "DELETE");
            return _cosmosDbService.DeleteItemAsync(id);
        }

        [HttpGet("{id}")]
        public Task<Claim> GetAsync(string id)
        {
            return _cosmosDbService.GetClaimAsync(id);
        }

        #endregion
    }
}