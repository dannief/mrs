using System.Collections.Generic;
using MRS.Domain.Interfaces;

namespace MRS.Domain
{
    public class Location
    {
        public static readonly NullLocation None = new NullLocation();

        public virtual string ID { get; set; }

        public virtual string Name { get; set; }

        public virtual Location ParentLocation { get; set; }

        public virtual bool IsNull { get { return false; } }

        public Location() {}

        public Location (string Id, string name, Location parentLocation = null)
        {
            this.ID = Id;
            this.Name = name;
            this.ParentLocation = parentLocation;
        }

        public ICollection<Location> Flatten()
        {
            var locations = new List<Location>();

            var tempLocation = this;
            while (tempLocation != null && !tempLocation.IsNull)
            {
                locations.Add(tempLocation);
                tempLocation = tempLocation.ParentLocation;
            }

            for (int i = locations.Count - 1; i >= 0; i--)
            {
                locations[i].ParentLocation = null;
            }

            return locations;
        }

        public override string ToString()
        {
            return Name + (ParentLocation == null || ParentLocation.IsNull ? string.Empty : ", " + ParentLocation.ToString());
        }
           
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Location))
                return false;

            var locationToCompare = obj as Location;
           
            return locationToCompare == this;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
                     
        public static bool operator ==(Location obj1, Location obj2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)obj1 == null) || ((object)obj2 == null))
            {
                return false;
            }

            return obj1.ID == obj2.ID;
        }

        public static bool operator !=(Location obj1, Location obj2)
        {            
            return !(obj1 == obj2);
        }

        public class NullLocation : Location
        {
            public override string ID { get { return string.Empty; } }
            public override string Name { get { return string.Empty; } }
            public override bool IsNull { get { return true; } }

            public override string ToString()
            {
                return "No Location";
            }
        }
    } 
}
