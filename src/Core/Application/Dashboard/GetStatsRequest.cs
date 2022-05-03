﻿using RewardsPlus.Application.Identity.Roles;
using RewardsPlus.Application.Identity.Users;

namespace RewardsPlus.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Brand> _brandRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer<GetStatsRequestHandler> _localizer;

    public GetStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Brand> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<GetStatsRequestHandler> localizer)
    {
        _userService = userService;
        _roleService = roleService;
        _brandRepo = brandRepo;
        _productRepo = productRepo;
        _localizer = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            BrandCount = await _brandRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken)
        };

        int selectedYear = DateTime.Now.Year;
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];
        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01);
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _brandRepo.CountAsync(brandSpec, cancellationToken);
            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Products"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Brands"], Data = brandsFigure });

        return stats;
    }
}