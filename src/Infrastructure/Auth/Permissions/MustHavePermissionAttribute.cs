using Microsoft.AspNetCore.Authorization;
using RewardsPlus.Shared.Authorization;

namespace RewardsPlus.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = FSHPermission.NameFor(action, resource);
}