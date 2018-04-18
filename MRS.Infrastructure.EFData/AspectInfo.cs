using PostSharp.Extensibility;
using MRS.Infrastructure.Aspects;

[assembly: CacheAspectAttribute(
    AttributeTargetTypes = "MRS.Infrastructure.EFData.LookupRepository", 
    AttributeTargetMemberAttributes = MulticastAttributes.Public )
]
[assembly: AuditAspectAttribute(
    AttributeTargetTypes = "MRS.Infrastructure.EFData.RequestRepository", 
    AttributeTargetMemberAttributes = MulticastAttributes.Public,
    AttributeTargetMembers = "regex:Save.*|Update.*")
]