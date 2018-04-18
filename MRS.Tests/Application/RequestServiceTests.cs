using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using MRS.DataTransferObjects;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Application;
using Xunit;

namespace MRS.Tests.Unit.Application
{
    public class RequestServiceTests 
    {
        RequestService _requestService;
        IUserRepository _userRepository;
        IRequestRepository _requestRepository;
        ILookupRepository _lookupRepository;

        public RequestServiceTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _requestRepository = A.Fake<IRequestRepository>();
            _lookupRepository = A.Fake<ILookupRepository>();
            _requestService = new RequestService(_requestRepository, _userRepository, _lookupRepository);
        }

        public class TheCreateRequestMethod : RequestServiceTests
        {            
            [Fact (Skip="TBD")]
            public void CreatesRquestWithLocationToServiceObject()
            {
              
            }

            [Fact (Skip="TBD")]
            public void CreatesRquestWithMaintenanceCategoryObject()
            {

            }
        }

        
    }
}
