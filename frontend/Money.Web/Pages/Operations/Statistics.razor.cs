﻿using Microsoft.AspNetCore.Components;
using Money.Web.Models.Charts;

namespace Money.Web.Pages.Operations;

public partial class Statistics
{
    private Dictionary<int, BarChart>? _barCharts;
    private Dictionary<int, PieChart>? _pieCharts;
    private List<Category>? _categories;

    private Dictionary<int, List<TreeItemData<OperationCategorySum>>> Sums { get; } = [];

    [Inject]
    private CategoryService CategoryService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Dictionary<int, BarChart> barCharts = [];
        Dictionary<int, PieChart> pieCharts = [];

        foreach (var operationType in OperationTypes.Values.Select(x => x.Id))
        {
            barCharts.Add(operationType, BarChart.Create(operationType));
            pieCharts.Add(operationType, PieChart.Create(operationType));
        }

        _barCharts = barCharts;
        _pieCharts = pieCharts;
        _categories = await CategoryService.GetAllAsync();
    }

    protected override void OnSearchChanged(object? sender, OperationSearchEventArgs args)
    {
        List<Task> tasks = [];

        var operationGroups = args.Operations?
            .GroupBy(x => x.Category.Id!.Value)
            .ToDictionary(x => x.Key, x => x.ToArray());

        foreach (var operationTypeId in OperationTypes.Values.Select(x => x.Id))
        {
            var categories = args.Operations?
                                 .Where(x => x.Category.OperationType.Id == operationTypeId)
                                 .Select(x => x.Category)
                                 .DistinctBy(x => x.Id)
                                 .ToList()
                             ?? [];

            tasks.Add(_barCharts![operationTypeId].UpdateAsync(args.Operations, categories, OperationsFilter.DateRange));

            if (_categories is not { Count: not 0 } || operationGroups == null)
            {
                continue;
            }

            var cats = _categories.Where(x => x.OperationType.Id == operationTypeId).ToList();
            var categorySums = CalculateCategorySums(cats, operationGroups, null);

            tasks.Add(_pieCharts![operationTypeId].UpdateAsync(categorySums));
            var sums = BuildChildren(categorySums);

            Sums[operationTypeId] =
            [
                new TreeItemData<OperationCategorySum>
                {
                    Value = new()
                    {
                        Name = "Всего",
                        TotalSum = sums.Sum(x => x.Value?.TotalSum ?? 0),
                    },
                    Children = sums,
                },
            ];
        }

        _ = Task.WhenAll(tasks);
        StateHasChanged();
    }

    private List<TreeItemData<OperationCategorySum>> BuildChildren(List<OperationCategorySum> categories)
    {
        return
        [
            .. categories.Where(x => x.TotalSum > 0)
                .Select(child => new TreeItemData<OperationCategorySum>
                {
                    Text = child.Name,
                    Value = child,
                    Children = child.SubCategories == null ? null : BuildChildren(child.SubCategories),
                })
                .OrderBy(item => item.Value?.TotalSum)
                .ThenBy(item => item.Value?.Name),
        ];
    }

    private List<OperationCategorySum> CalculateCategorySums(List<Category> categories, Dictionary<int, Operation[]> operations, int? parentId)
    {
        var categorySums = new List<OperationCategorySum>();

        foreach (var category in categories.Where(x => x.ParentId == parentId))
        {
            var totalMainSum = category.Id != null && operations.TryGetValue(category.Id.Value, out var operationGroup)
                ? operationGroup.Sum(op => op.Sum)
                : 0;

            var childCategorySums = CalculateCategorySums(categories, operations, category.Id);

            var operationCategorySum = new OperationCategorySum
            {
                Name = category.Name,
                Color = category.Color,
                ParentId = parentId,
                TotalSum = totalMainSum + childCategorySums.Sum(x => x.TotalSum),
                SubCategories = childCategorySums,
            };

            if (childCategorySums.Count > 0)
            {
                operationCategorySum.SubCategories.Add(new()
                {
                    Name = category.Name,
                    TotalSum = totalMainSum,
                });
            }

            categorySums.Add(operationCategorySum);
        }

        return categorySums;
    }
}
