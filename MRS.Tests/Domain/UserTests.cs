using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.DataTransferObjects;
using MRS.Domain;
using MRS.Domain.Interfaces;
using Xunit;

namespace MRS.Tests.Unit.Domain
{
    public class UserTests
    {
        protected User _user;
        protected Room _room;
        protected Category _category;

        public UserTests()
        {
            _room = new Room("Mary Secole Room 1", null);
            _user = new Student { Tenancies = { _room } };
            _category = new Category("Furniture", "Furniture", Category.None);
        }

        public class TheCreateRequestMethod : UserTests
        {            
            [Fact]
            public void ShouldThrowExceptionIfLocationToServiceIsNull()
            {
                Assert.Throws<ApplicationException>(
                    delegate
                    {
                        var request = _user.CreateRequest("", "", null, _category, Severity.Critical);
                    });
            }

            [Fact]
            public void ShouldThrowExceptionIfLocationToServiceIsNullLocation()
            {
                Request request;

                Assert.Throws<ApplicationException>(
                    delegate
                    {
                        request = _user.CreateRequest("", "", Location.None, _category, Severity.Critical);
                    });                                
            }

            [Fact]
            public void ShouldEnsureRequestHasLocationToService()
            {
                var request = _user.CreateRequest("", "", _room, _category, Severity.Critical);

                Assert.Equal(_room, request.LocationToService);
            }

            [Fact]
            public void ShouldEnsureRequestRequesterNotNull()
            {
                var request = _user.CreateRequest("", "", _room, _category, Severity.Critical);
                
                Assert.Equal(_user, request.Requester);
            }
                        
            [Fact]
            public void ShouldEnsureRequestCategoryNotNull()
            {
                var request = _user.CreateRequest("", "", _room, _category, Severity.Critical);

                Assert.Equal(_category, request.Category);
            }

            [Fact]
            public void ShouldThrowExceptionIfMaintenanceCategoryIsNullCategory()
            {
                Assert.Throws<ApplicationException>(
                    delegate
                    {
                        var request = _user.CreateRequest("", "", _room, Category.None, Severity.Critical);
                    });
            }

        }

        public class TheUpdateRequestMethod : UserTests
        {

        }
    }
}
