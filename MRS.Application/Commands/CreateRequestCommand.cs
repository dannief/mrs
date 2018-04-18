using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Application.Command
{
    public class CreateRequestCommand
    {
        public Guid RequestNumber { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string LocationID { get; private set; }

        public string SubCategoryID { get; private set; }

        public string RequesterIDNumber { get; private set; }

        public string SeverityID { get; private set; }

        public CreateRequestCommand(Guid requestNumber, string title, string description, string locationID, string subCategoryID, string requesterIDNumber, string severityID)
        {
            this.RequestNumber = requestNumber;
            this.Title = title;
            this.Description = description;
            this.LocationID = locationID;
            this.SubCategoryID = subCategoryID;            
            this.RequesterIDNumber = requesterIDNumber;
            this.SeverityID = severityID;
        }

    }
}
