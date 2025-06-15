using IdentityService.Domain.Enums;
using Shared.DataAccess.Common;

namespace IdentityService.Domain.Entities;

public class OperationClaim : Entity
{
    // change type to array
    public Role Role { get; set; } = Role.User;
    public ICollection<UserOperationClaim> UserOperationClaim { get; set; }

    public OperationClaim()
    {
        UserOperationClaim = new HashSet<UserOperationClaim>();
    }

    public OperationClaim(Role role) : this()
    {
        Role = role;
    }

    public OperationClaim(Guid id,Role role) : this()
    {
        Id = id;
        Role = role;
    }
}