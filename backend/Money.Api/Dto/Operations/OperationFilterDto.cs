﻿namespace Money.Api.Dto.Operations;

/// <summary>
///     Фильтр для операций.
/// </summary>
public class OperationFilterDto
{
    /// <summary>
    ///     Дата начала периода.
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    ///     Дата окончания периода.
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    ///     Список идентификаторов категорий (цифры через запятую. Пример: "1,2,5").
    /// </summary>
    public string? CategoryIds { get; set; }

    /// <summary>
    ///     Комментарий.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     Место.
    /// </summary>
    public string? Place { get; set; }

    public OperationFilter ToBusinessModel()
    {
        return new OperationFilter
        {
            CategoryIds = ParseCategoryIds(CategoryIds),
            Comment = Comment,
            Place = Place,
            DateFrom = DateFrom,
            DateTo = DateTo,
        };
    }

    private static List<int>? ParseCategoryIds(string? categoryIds)
    {
        if (string.IsNullOrWhiteSpace(categoryIds))
        {
            return null;
        }

        return categoryIds.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(id => int.TryParse(id, out int parsedId) ? (int?)parsedId : null)
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .ToList();
    }
}
