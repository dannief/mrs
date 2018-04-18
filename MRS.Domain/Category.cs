using System.Collections.Generic;
using MRS.Domain.Interfaces;

namespace MRS.Domain
{
    public class Category
    {
        public static readonly NullCategory None = new NullCategory();

        public virtual string ID { get; set; }

        public virtual string Name { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual bool IsNull { get { return false; } }

        public Category()
        {
        }

        public Category(string id, string name, Category parentCategory = null)
        {
            this.ID = id;
            this.Name = name;
            this.ParentCategory = parentCategory;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Category))
                return false;

            var categoryToCompare = obj as Category;

            return categoryToCompare.ID == this.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public static bool operator ==(Category obj1, Category obj2)
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

        public static bool operator !=(Category obj1, Category obj2)
        {
            return !(obj1 == obj2);
        }

        public class NullCategory : Category
        {
            public override string ID { get { return string.Empty; } }

            public override string Name { get { return string.Empty; } }

            public override bool IsNull { get { return true; } }

            public override bool Equals(object obj)
            {
                return obj is NullCategory;
            }

            public override int GetHashCode()
            {
                return 0;
            }

            public override string ToString()
            {
                return "No Category";
            }
        }
    }
}