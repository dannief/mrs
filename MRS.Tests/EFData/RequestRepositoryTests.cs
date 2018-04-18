using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Domain.States;
using MRS.Infrastructure.EFData;
using Xunit;

namespace MRS.Tests.Unit.EFData
{
    public class RequestRepositoryTests : IDisposable
    {
        MrsContext _context;
        IRequestRepository _requestRepository;
       
        public RequestRepositoryTests()
        {
            _context = new MrsContext();
            _requestRepository = new RequestRepository(_context);      
        }

        public void Dispose()
        {
                       
        }

        public class TheGetByRequestNumberMethod : RequestRepositoryTests
        {
            [Fact]
            public void CanGetRequest()
            {
                // setup request in db for testing
                var insertedRequestNumber = Guid.NewGuid().ToString();
                var insertRequestSql =
                    @"INSERT [dbo].[Requests] ([RequestNumber], [Title], [Description], [RequestDate], [Severity], [Category_ID], [Requester_IDNumber], [LocationToService_ID], [State_ID], [WorkOrder_WorkOrderNumber]) 
                VALUES (N'" + insertedRequestNumber + "', N'Test Request', N'Test Request Description', CAST(0x0000A20200EB1588 AS DateTime), 0, N'FIX_DOOR', N'ST-00003', N'HALL_IRV_DM1', N'New', NULL)";
                // FAC-SCI_CHEM_CLT               
                _context.Database.ExecuteSqlCommand(insertRequestSql);

                var request = _requestRepository.GetByRequestNumber(insertedRequestNumber);
                Assert.Equal(insertedRequestNumber, request.RequestNumber.ToString());
                Assert.NotNull(request.Category);
                Assert.NotNull(request.LocationToService);
                Assert.NotNull(request.Requester);
                Assert.NotNull(request.State);
                Assert.NotNull(request.State.Request);

                // check that null location object is set
                Assert.NotNull(request.LocationToService.ParentLocation.ParentLocation);
                // check that null category object is set
                Assert.NotNull(request.Category.ParentCategory.ParentCategory);

                // delete sql-inserted request
                var deleteInsertedRequestSql = "DELETE FROM [dbo].[Requests] WHERE [RequestNumber] = '" + insertedRequestNumber + "'";
                _context.Database.ExecuteSqlCommand(deleteInsertedRequestSql);

            }
        }

        public class TheSaveMethod : RequestRepositoryTests
        {
            [Fact]
            public void CanCreateNewRequest()
            {
                var request = new Request
                {
                    Category = new Category { ID = "FIX_DOOR", ParentCategory = Category.None },
                    Description = "Test Request Description",
                    LocationToService = new Location { ID = "HALL_IRV_DM1", ParentLocation = Location.None },
                    Requester = new Student { IDNumber = "ST-00003" },
                    Severity = Severity.Low,
                    Title = "Test Request",
                    RequestDate = DateTime.Now                    
                };

                request.State = new NewState(request);
                                
                _requestRepository.SaveRequest(request);
               
                Assert.NotEqual(Guid.Empty, request.RequestNumber);

                // delete code-created request
                var createdRequestNumber = request.RequestNumber.ToString();               
                var deleteCreatedRequestSql = "DELETE FROM [dbo].[Requests] WHERE [RequestNumber] = '" + createdRequestNumber + "'";
                _context.Database.ExecuteSqlCommand(deleteCreatedRequestSql);
            }

            [Fact(Skip="TBD")]
            public void CanUpdateExistingRequest()
            {
                
            }
        }
    }
}
