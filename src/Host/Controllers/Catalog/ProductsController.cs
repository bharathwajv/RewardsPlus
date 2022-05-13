using RewardsPlus.Application.Catalog.Products;

namespace RewardsPlus.Host.Controllers.Catalog;

public class ProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Products)]
    [OpenApiOperation("Search products using available filters.", "")]
    public Task<PaginationResponse<ProductDto>> SearchAsync(SearchProductsRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details.", "")]
    public Task<ProductDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetProductRequest(id), cancellationToken);
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details via dapper.", "")]
    public Task<ProductDto> GetDapperAsync(Guid id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new GetProductViaDapperRequest(id), cancellationToken);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Products)]
    [OpenApiOperation("Create a new product.", "")]
    public Task<Guid> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Products)]
    [OpenApiOperation("Update a product.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductRequest request, Guid id, CancellationToken cancellationToken)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Products)]
    [OpenApiOperation("Delete a product.", "")]
    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return Mediator.Send(new DeleteProductRequest(id), cancellationToken);
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Products)]
    [OpenApiOperation("Export a products.", "")]
    public async Task<FileResult> ExportAsync(ExportProductsRequest filter, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(filter, cancellationToken);
        return File(result, "application/octet-stream", "ProductExports");
    }
}