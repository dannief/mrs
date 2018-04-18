using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Application.Interfaces;
using MRS.DataTransferObjects;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Domain.States;

namespace MRS.Application
{
    public class LookupService : ILookupService
    {
        private IRequestRepository requests;
        private ILookupRepository lookupData;
        private IUserRepository users;

        public LookupService(IRequestRepository requests, IUserRepository users, ILookupRepository lookupData)
        {
            this.requests = requests;
            this.users = users;
            this.lookupData = lookupData;
        }

        public ICollection<LookupDataDto> GetLocationsUserCanCreateRequestFor(string userIdNumber)
        {
            var userRequester = users.GetByIDNumber(userIdNumber);
            // TODO: Pass filter to repository
            return lookupData.GetLocations()
                .Where(x => userRequester.CanCreateRequestForLocation(x))
                .Select(x => new LookupDataDto
                {
                    ID = x.ID,
                    Name = x.ToString()
                })
                .ToList();
        }

        public ICollection<LookupDataDto> GetMainCategories()
        {
            return lookupData.GetCategories()
                .Where(x => x.ParentCategory.IsNull)
                .Select(x => new LookupDataDto
                {
                    ID = x.ID,
                    Name = x.Name
                })
                .ToList();
        }

        public ICollection<LookupDataDto> GetSubCategories(string mainCategoryId)
        {
            return lookupData.GetCategories()
                .Where(x => x.ParentCategory.ID == mainCategoryId)
                .Select(x => new LookupDataDto
                {
                    ID = x.ID,
                    Name = x.Name
                })
                .ToList();
        }

        public ICollection<LookupDataDto> GetSeverities()
        {
            var enumValues = Enum.GetValues(typeof(Severity)) as Severity[];
            return enumValues.Select(x => new LookupDataDto { ID = ((int)x).ToString(), Name = x.ToString() }).ToList();
        }

        public ICollection<LookupDataDto> GetPriorities()
        {
            var enumValues = Enum.GetValues(typeof(Priority)) as Priority[];
            return enumValues.Select(x => new LookupDataDto { ID = ((int)x).ToString(), Name = x.ToString() }).ToList();
        }

        public ICollection<LookupDataDto> GetWorkers(string locationID)
        {
            var location = lookupData.GetLocations().Single(x => x.ID == locationID);

            var workers = users.GetWorkersForLocation(location);

            return workers
                .Select(x => new LookupDataDto
                {
                    ID = x.IDNumber,
                    Name = x.Name
                })
                .ToList();
        }


        // TODO: Move to domain
        public ICollection<LookupDataDto> GetNextPossibleStates(string loggedInUserIDNumber, string requestNumber)
        {
            // TODO: add StateDomainService takes current request and user and returns the next possible states
            var user = users.GetByIDNumber(loggedInUserIDNumber);
            var request = requests.GetByRequestNumber(requestNumber);
            var states = request.State.GetNextPossibleStates().Cast<RequestState>();
            states = states.Where(x => user.CanChangeToState(x));
            return states.Select(x => new LookupDataDto { ID = x.ID, Name = x.Name }).ToList();
        }
    }
}
