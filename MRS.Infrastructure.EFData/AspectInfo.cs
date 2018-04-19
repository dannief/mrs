using PostSharp.Extensibility;
using MRS.Infrastructure.Aspects;

[assembly: CacheAspect(
    AttributeTargetTypes = "MRS.Infrastructure.EFData.LookupRepository", 
    AttributeTargetMemberAttributes = MulticastAttributes.Public )
]
[assembly: AuditAspect(
    AttributeTargetTypes = "MRS.Infrastructure.EFData.RequestRepository", 
    AttributeTargetMemberAttributes = MulticastAttributes.Public,
    AttributeTargetMembers = "regex:Save.*|Update.*")
]