using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain;
using Newtonsoft.Json;
using PostSharp;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace MRS.Infrastructure.Aspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class AuditAspectAttribute : OnMethodBoundaryAspect
    {
        #region Instance variables
        private string methodName;
        private Type className;
        private IList<string> parameterNames;
        private int requestNumberParameterIndex = -1; 
        #endregion

        public override bool CompileTimeValidate(MethodBase method)
        {
            if (requestNumberParameterIndex == -1) {
                Message.Write(MessageLocation.Unknown, SeverityType.Warning, "001", 
                    "AuditAspect was unable to find a requestNumber or request parameter in {0}.{1}", className,methodName);
                return false;
            }
            return true;
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            methodName = method.Name;
            className = method.DeclaringType;
            parameterNames = method.GetParameters().Select(x => x.Name).ToList();
            for (int i = 0; i < parameterNames.Count; i++) {
                if (parameterNames[i] == "requestNumber" || parameterNames[i] == "request") {
                    requestNumberParameterIndex = i;
                    break;
                }
            }
        }
                
        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (requestNumberParameterIndex != -1 && parameterNames != null)
            {
                var parameters = new Dictionary<string, object>();
                var requestNumberArg = args.Arguments[requestNumberParameterIndex];
                var requestNumber = requestNumberArg is Request ? ((Request)requestNumberArg).RequestNumber.ToString() : 
                    requestNumberArg.ToString();                                
                var arguments = args.Arguments.ToList();

                for (int i = 0; i < parameterNames.Count; i++) {
                    parameters.Add(parameterNames[i], arguments[i]);
                }

                string json = SerializeToJson(parameters);

                SaveAuditLog(requestNumber, json);
            }
        }

        private static void SaveAuditLog(string requestNumber, string json)
        {
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
}
