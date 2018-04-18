using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.DataTransferObjects;

namespace MRS.Application.Interfaces
{
    public interface ILookupService
    {
        ICollection<LookupDataDto> GetLocationsUserCanCreateRequestFor(string userIdNumber);
        ICollection<LookupDataDto> GetMainCategories();
        ICollection<LookupDataDto> GetSubCategories(string mainCategoryId);
        ICollection<LookupDataDto> GetSeverities();
        ICollection<LookupDataDto> GetWorkers(string locationID);
        ICollection<LookupDataDto> GetPriorities();
        ICollection<LookupDataDto> GetNextPossibleStates(string loggedInUserIDNumber, string requestNumber); 
    }
}
