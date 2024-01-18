using Claims.Models;
using Claims.Services;
using Moq;
using Xunit;

namespace Claims.Tests
{
    public class ClaimsServiceTests
    {
       public ClaimsServiceTests()
        {
            // interface mocking should be here
            // service = new Mock<Service> like a cinstructor 

            //  remaining methods
        }

        [Fact]

        public void  Should_be_Egual()
        {
            
            var cosmoDbService = new Mock<ICosmoDbService>(); 

            var claim = new ClaimsService(cosmoDbService.Object);

            var expectedClaims = new List<Claim> 
            {
               new Claim
               {
                   Id = "ID",
                   CoverId = "1",
                   DamageCost = 1,
                   Created = DateTime.Now,
                   Name = "Name",
                   Type = ClaimType.Fire
               }
            };

            cosmoDbService.Setup(c => c.GetClaimsAsync())
                .ReturnsAsync(expectedClaims);

            var results = claim.GetClaimsAsync().Result.ToList();
            
            Assert.Equal(expectedClaims, results);
        }
    }
}
