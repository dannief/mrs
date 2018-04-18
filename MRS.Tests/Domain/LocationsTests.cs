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
    public class LocationsTests
    {
        Location _room1ForEqualTest;
        Location _room2ForEqualTest;
        Location _room3ForNotEqualTest;

        string _room1ForEqualTestToString = "Room 1, Building 1, Department 1";

        public LocationsTests()
        {
            var department1 = new Location("D1", "Department 1", Location.None);
            var building1 = new Location("B1D1", "Building 1", department1);
            _room1ForEqualTest = new Location("R1B1D1", "Room 1", building1);

            var department2 = new Location("D1", "Department 1", Location.None);
            var building2 = new Location("B1D1", "Building 1", department2);
            _room2ForEqualTest = new Location("R1B1D1", "Room 1", building2);

            var building3 = new Location("B1", "Building 1");
            _room3ForNotEqualTest = new Location("R1B1", "Room 1", building3);
        }

        public class TheEqualsMethod : LocationsTests
        {
            [Fact]
            public void ShouldReturnTrueIfLocationPropertiesEqual()
            {
                Assert.True(_room1ForEqualTest.Equals(_room2ForEqualTest));
            }

            [Fact]
            public void ShouldReturnFalseIfOneLocationIsNullAndTheOtherNonNull()
            {                
                Assert.False(_room1ForEqualTest.Equals(_room3ForNotEqualTest));
            }
        }

        public class TheToStringMethod : LocationsTests
        {
            [Fact]
            public void CanReturnALocationString()
            {
                Assert.Equal(_room1ForEqualTestToString, _room1ForEqualTest.ToString());
            }
        }

        public class TheGetHashCodeMethod : LocationsTests
        {
            [Fact]
            public void ShouldReturnHashCodeBasedOnLocationString()
            {
                Assert.Equal(_room1ForEqualTestToString.GetHashCode(), _room1ForEqualTest.GetHashCode());
            }
        }
    }
}
