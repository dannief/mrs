using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Audit;
using EntityFramework.Extensions;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Infrastructure.EFData;
using Newtonsoft.Json;

namespace MRS.Infrastructure.Patterns
{
    public class RequestRepositoryAuditDecorator : IRequestRepository
    {
        private IRequestRepository repository;
        private MrsContext context;
        
        public RequestRepositoryAuditDecorator(IRequestRepository repository, MrsContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public Request GetByRequestNumber(string requestNumber)
        {
            return repository.GetByRequestNumber(requestNumber);
        }

        public ICollection<Request> GetRequests(Func<Request, bool> filter)
        {
            return repository.GetRequests(filter);
        }

        public void SaveRequest(Request request)
        {
            Audit(() => repository.SaveRequest(request), request.RequestNumber.ToString());
        }

        public Request GetByRequestNumber(string requestNumber, Func<Request, bool> filter)
        {
            throw new NotImplementedException();
        }
      
        private void Audit(Action action, string requestNumber)
        {
            var audit = context.BeginAudit();

            action();

            SaveAuditLog(audit, requestNumber);

            context.SaveChanges();
        }               

        void SaveAuditLog(AuditLogger audit, string requestNumber)
        {
            var log = audit.LastLog;
            var userIDNumber = Thread.CurrentPrincipal.Identity.Name;
            log.Username = userIDNumber;

            var entitiesChanged = log.Entities.Select(x => x.Current);

            string json = SerializeToJson(entitiesChanged);

            Debug.WriteLine(json);
        }

        public static string SerializeToJson(object toSerialize)
        {
            return JsonConvert.SerializeObject(
                toSerialize,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

    }

    internal class MrsAuditConfiguration
    {
        static MrsAuditConfiguration()
        {
            var config = AuditConfiguration.Default;

            config.IncludeRelationships = true;
            config.LoadRelationships = false;
            config.DefaultAuditable = true;
        }

        public static void Initialize()
        {

        }
    }
}
