using RewardsPlus.Application.Token;

namespace RewardsPlus.Host.Controllers.Multitenancy;

public class TokensController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Tokens)]
    [OpenApiOperation("Search tokens.", "")]
    public Task<List<TokenDto>> GetListAsync()
    {
        return Mediator.Send(new GetAllTokensRequest());
    }

    //[HttpGet("{id}")]
    //[MustHavePermission(FSHAction.View, FSHResource.Tokens)]
    //[OpenApiOperation("View tokens by user id.", "")]
    //public Task<TokenDto> GetAsync(string id)
    //{
    //    return Mediator.Send(new GetTenantRequest(id));
    //}

    [HttpPost("gift")]
    [MustHavePermission(FSHAction.GiftToken, FSHResource.Tokens)]
    [OpenApiOperation("GiftToken to other users.", "")]
    public Task<string> GiftTokenAsync(GiftTokensRequest request)
    {
        return Mediator.Send(request);
    }

}