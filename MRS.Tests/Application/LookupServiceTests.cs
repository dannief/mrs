using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using MRS.Application;
using MRS.DataTransferObjects;
using MRS.Domain;
using MRS.Domain.Interfaces;
using Xunit;

namespace MRS.Tests.Application
{
    public class LookupServiceTests
    {

        LookupService _lookupService;
        IUserRepository _userRepository;
        IRequestRepository _requestRepository;
        ILookupRepository _lookupRepository;

        public LookupServiceTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _requestRepository = A.Fake<IRequestRepository>();
            _lookupRepository = A.Fake<ILookupRepository>();
            _lookupService = new LookupService(_requestRepository, _userRepository, _lookupRepository);
        }

        public class TheGetSeveritiesMethod : LookupServiceTests
        {
            [Fact]
            public void ReturnsAllSeverities()
            {
                var expectedSeverties = new List<LookupDataDto> 
                {
                    new LookupDataDto { ID = ((int)Severity.Low).ToString(), Name = Severity.Low.ToString()},
                    new LookupDataDto { ID = ((int)Severity.Medium).ToString(), Name = Severity.Medium.ToString()},
                    new LookupDataDto { ID = ((int)Severity.High).ToString(), Name = Severity.High.ToString()},
                    new LookupDataDto { ID = ((int)Severity.Critical).ToString(), Name = Severity.Critical.ToString()}
                };

                var actualSeverities = _lookupService.GetSeverities();

                // Check that all actual severities are in the expected severities list;
                Assert.True(actualSeverities.All(a => expectedSeverties.Any(e => e.ID == a.ID)));
            }
        }
    }
}
