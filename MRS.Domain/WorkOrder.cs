using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain
{
    public class WorkOrder
    {
        public static readonly NullWorkOrder None = new NullWorkOrder();

        public virtual Guid WorkOrderNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual User AssignedWorker { get; set; }
        public virtual Priority Priority { get; set; }

        public virtual bool IsNull { get { return false; } }

        public override string ToString()
        {
            return WorkOrderNumber.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is WorkOrder))
                return false;

            var objToCompare = obj as WorkOrder;

            return objToCompare == this;
        }

        public override int GetHashCode()
        {
            return this.WorkOrderNumber.GetHashCode();
        }

        public static bool operator ==(WorkOrder obj1, WorkOrder obj2)
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

            return obj1.WorkOrderNumber == obj2.WorkOrderNumber;
        }

        public static bool operator !=(WorkOrder obj1, WorkOrder obj2)
        {
            return !(obj1 == obj2);
        }

        public class NullWorkOrder : WorkOrder
        {            
            public override string Description { get { return string.Empty; } }
            public override bool IsNull { get { return true; } }
            public override User AssignedWorker { get { return User.None; } }

            public override string ToString()
            {
                return "No WorkOrder";
            }
        }
    }        
}
