using Claims.Auditing;
using Claims.CosmoDb;
using Claims.Models;
using Claims.PremiumProvider;
using Claims.Services;
using Microsoft.Azure.Cosmos;
using Moq;
using Xunit;


namespace Claims.Tests
{
    public class CoversServicesTests
    {
        // interface mocking should be here
        // service = new Mock<Service> like a cinstructor 

        //TODO Test PRemium
        // Rearange setups, test is added for examplw, Mock of all interfaces should be addes

        //  remaining methods

        [Fact]

        public void Should_be_Egual()
        {

            var coverDbService = new Mock<ICoverDbService>();

            var premium = new Mock<IPremiumProvider>();

            ICoverDbService cover = coverDbService.Object;

            var coverExp = new List<Cover>
            {
               new Cover
               {
                   Id = "ID",

                   StartDate = DateOnly.Parse("01/01/2024"),
                   EndDate =  DateOnly.Parse("01/03/2024"),
                   Premium = 1.5M,
                   Type = CoverType.Tanker
               }
            };

            coverDbService.Setup(c => c.GetAsync())
                .ReturnsAsync(coverExp);

            var results = cover.GetAsync().Result.ToList();

            Assert.Equal(coverExp, results);
        }
    }
}
