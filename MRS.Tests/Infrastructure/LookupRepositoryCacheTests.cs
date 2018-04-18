using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Infrastructure.Common.Cache;
using MRS.Infrastructure.CrossCutting;
using Xunit;

namespace MRS.Tests.Infrastructure
{
    public class LookupRepositoryCacheTests
    {
        public class TheGetLocationsMethod
        {
            [Fact]
            public void CanReturnLocationsFromTheCache()
            {
                // Arrange  
                var testLocations = 
                    new List<Location> 
                    { 
                        new Location { ID = "Loc1", Name = "Location 1", ParentLocation = Location.None } 
                    };

                var lookupRepo = A.Fake<ILookupRepository>();
                var cache = new MemoryCacheProvider();
                var lookupRepoCache = new LookupRepositoryCacheDecorator(lookupRepo, cache);

                A.CallTo(() => lookupRepo.GetLocations()).Returns(testLocations).Twice();

                // Act
                // A call to GetLocations() from decorator should return locations from repository
                var locations = lookupRepoCache.GetLocations();
                // A call to GetLocations() from decorator should return locations from cache after first call
                locations = lookupRepoCache.GetLocations();

                // Assert
                // LookupRepository should be called once
                A.CallTo(() => lookupRepo.GetLocations()).MustHaveHappened(Repeated.Exactly.Once);
                // Check that locations are returned even though the LookupRepository.GetLocations()
                // method was not called.
                Assert.Equal(1, locations.Count());
            }
        }
    }
}
