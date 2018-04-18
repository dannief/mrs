using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Infrastructure.EFData;
using Xunit;

namespace MRS.Tests.Infrastructure
{
    public class LookupRepositoryTests
    {       
        public class TheGetLocationsMethod : LookupRepositoryTests
        {
            [Fact]
            public void HasCacheAspectApplied()
            {   
                // Arrange
                var _context = new MrsContext();
                var _lookupRepository1 = new LookupRepository(_context);
                var _lookupRepository2 = new LookupRepository(null);

                // Act
                // A call to GetLocations() should return locations from repository
                var locations1 = _lookupRepository1.GetLocations();
                // A call to GetLocations() should return locations from cache
                // If not a exception is expected since db context is null
                var locations2 = _lookupRepository2.GetLocations();

                // Assert
                Assert.True(locations1.Count() > 0);
                Assert.Equal(locations1.Count(), locations2.Count());
            }
        }
    }
}
