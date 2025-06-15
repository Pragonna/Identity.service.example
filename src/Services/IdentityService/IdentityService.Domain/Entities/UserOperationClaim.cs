using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Entities;

public class UserOperationClaim
{
    public Guid UserId { get; set; }
    public Guid OperationClaimId { get; set; }
    public User User { get; set; }
    public OperationClaim OperationClaim { get; set; }

    public UserOperationClaim(Guid userId, Guid operationClaimId)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}