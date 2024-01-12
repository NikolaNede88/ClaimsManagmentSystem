using Claims.Auditing;
using Claims.Models;
using Claims.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    #region Properties

    private readonly ILogger<CoversController> _logger;

    private readonly Auditer _auditer;

    private readonly Container _container;

    private readonly ICoversServiceInterface _coversServiceInterface;

    #endregion

    #region Constructor

    public CoversController(CosmosClient cosmosClient, AuditContext auditContext, ILogger<CoversController> logger, ICoversServiceInterface coversServiceInterface)
    {
        _logger = logger;
        _auditer = new Auditer(auditContext);
        _container = cosmosClient?.GetContainer("ClaimDb", "Cover")
                     ?? throw new ArgumentNullException(nameof(cosmosClient));
        _coversServiceInterface = coversServiceInterface;
    }

    #endregion

    #region Methods  

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
        var results = await _coversServiceInterface.GetAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        try
        {
            var response = await _coversServiceInterface.GetAsync(id);

            return Ok(response);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Cover cover)
    {
        var result = await _coversServiceInterface.CreateAsync(cover);
       
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
         await _coversServiceInterface.DeleteAsync(id);      
    }

    #endregion
}