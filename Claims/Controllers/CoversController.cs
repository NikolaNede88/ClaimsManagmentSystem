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
    private readonly ILogger<CoversController> _logger;
    private readonly Auditer _auditer;
    private readonly Container _container;
    private readonly ICoversServiceInterface _coversServiceInterface;

    public CoversController(CosmosClient cosmosClient, AuditContext auditContext, ILogger<CoversController> logger, ICoversServiceInterface coversServiceInterface)
    {
        _logger = logger;
        _auditer = new Auditer(auditContext);
        _container = cosmosClient?.GetContainer("ClaimDb", "Cover")
                     ?? throw new ArgumentNullException(nameof(cosmosClient));
        _coversServiceInterface = coversServiceInterface;
    }

    [HttpPost]
    public async Task<ActionResult> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        return Ok(ComputePremium(startDate, endDate, coverType));
    }

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

    private decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        var multiplier = 1.3m;
        if (coverType == CoverType.Yacht)
        {
            multiplier = 1.1m;
        }

        if (coverType == CoverType.PassengerShip)
        {
            multiplier = 1.2m;
        }

        if (coverType == CoverType.Tanker)
        {
            multiplier = 1.5m;
        }

        var premiumPerDay = 1250 * multiplier;
        var insuranceLength = endDate.DayNumber - startDate.DayNumber;
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLength; i++)
        {
            if (i < 30) totalPremium += premiumPerDay;
            if (i < 180 && coverType == CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.05m;
            else if (i < 180) totalPremium += premiumPerDay - premiumPerDay * 0.02m;
            if (i < 365 && coverType != CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.03m;
            else if (i < 365) totalPremium += premiumPerDay - premiumPerDay * 0.08m;
        }

        return totalPremium;
    }
}