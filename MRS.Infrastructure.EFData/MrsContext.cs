using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain;
using MRS.Domain.States;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MRS.Infrastructure.EFData
{
    public class MrsContext: DbContext
    {      
        public MrsContext()
            : base()
        {
            SetConfiguration();
        }
                
        private void SetConfiguration()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public void ChangeRelationshipState<TEntity>(TEntity sourceEntity, object targetEntity, Expression<Func<TEntity, object>> navigationPropertySelector, EntityState relationshipState)
            where TEntity : class
        {
            ObjectContext.ObjectStateManager.ChangeRelationshipState(sourceEntity, targetEntity, navigationPropertySelector, relationshipState);
        }

        public System.Data.Entity.Core.Objects.ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this).ObjectContext;
            }
        }
                
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Entity
            modelBuilder.Entity<User>().HasKey(x => x.IDNumber);
            modelBuilder.Entity<User>().HasMany(x => x.Tenancies).WithMany().Map(x => x.ToTable("Tenancies"));
            modelBuilder.Ignore<User.NullUser>();
            
            // Worker Entity
            modelBuilder.Entity<Worker>().HasMany(x => x.WorkCategories).WithMany();
           
            // Request Entity
            modelBuilder.Entity<Request>().HasKey(x => x.RequestNumber);
            modelBuilder.Ignore<Request.NullRequest>();
                        
            // Request State
            modelBuilder.Entity<RequestState>();
            modelBuilder.Entity<RequestState>().Ignore(p => p.Request);
            modelBuilder.Ignore<RequestState.NullRequestState>();
           
            // WorkOrder Entity
            modelBuilder.Entity<WorkOrder>().HasKey(x => x.WorkOrderNumber);
            modelBuilder.Ignore<WorkOrder.NullWorkOrder>();
            
            // Location Entity
            modelBuilder.Entity<Location>().HasOptional(x => x.ParentLocation).WithMany();
            modelBuilder.Ignore<Location.NullLocation>();

            // Category Entity
            modelBuilder.Entity<Category>().HasOptional(x => x.ParentCategory).WithMany(x => x.SubCategories);
            modelBuilder.Ignore<Category.NullCategory>();

            // Request Change Log
            modelBuilder.Entity<RequestChangeLog>();
        }
    }
}
