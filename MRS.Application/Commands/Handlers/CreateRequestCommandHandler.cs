using MRS.Application.Command;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Domain.Services;
using MRS.Infrastructure.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Application.Commands.Handlers
{
    public class CreateRequestCommandHandler : ICommandHandler<CreateRequestCommand>
    {
        private IRequestRepository requests;

        private IUserRepository users;

        private ILookupRepository lookupData;


        public CreateRequestCommandHandler(IRequestRepository requests, IUserRepository users, ILookupRepository lookupData)
        {
            this.requests = requests;
            this.users = users;
            this.lookupData = lookupData;
        }

        public void Handle(CreateRequestCommand command)
        {
            var userRequester = users.GetByIDNumber(command.RequesterIDNumber);
            var locationToService = lookupData.GetLocations().Single(x => x.ID == command.LocationID);
            var maintenanceCategory = lookupData.GetCategories().Single(x => x.ID == command.SubCategoryID);
            var severity = (Severity)int.Parse(command.SeverityID);

            var request = userRequester.CreateRequest(command.Title, command.Description, locationToService, maintenanceCategory, severity);

            requests.SaveRequest(request);
        }
    }
}
