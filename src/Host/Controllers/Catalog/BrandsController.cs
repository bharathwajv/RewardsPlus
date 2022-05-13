using RewardsPlus.Application.Catalog.Brands;

namespace RewardsPlus.Host.Controllers.Catalog;

public class BrandsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<BrandDto>> SearchAsync(SearchBrandsRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Get brand details.", "")]
    public Task<BrandDto> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetBrandRequest(id), cancellationToken);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Create a new brand.", "")]
    public Task<Guid> CreateAsync(CreateBrandRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Update a brand.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBrandRequest request, Guid id, CancellationToken cancellationToken)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Delete a brand.", "")]
    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new DeleteBrandRequest(id), cancellationToken);
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Brands)]
    [OpenApiOperation("Generate a number of random brands.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomBrandRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Brands)]
    [OpenApiOperation("Delete the brands generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(RPApiConventions), nameof(RPApiConventions.Search))]
    public Task<string> DeleteRandomAsync(CancellationToken cancellationToken)
    {
        return Mediator.Send(new DeleteRandomBrandRequest(), cancellationToken);
    }
}