﻿using Money.ApiClient;

namespace Money.Web.Services;

public class CategoryService(MoneyClient moneyClient)
{
    public async Task<List<Category>?> GetCategories()
    {
        ApiClientResponse<CategoryClient.Category[]> apiCategories = await moneyClient.Category.Get();

        return apiCategories.Content?.Select(apiCategory => new Category
            {
                Id = apiCategory.Id,
                ParentId = apiCategory.ParentId,
                Name = apiCategory.Name,
                Color = apiCategory.Color,
                Order = apiCategory.Order,
                OperationType = OperationTypes.Values.First(x => x.Id == apiCategory.OperationTypeId),
            })
            .ToList();
    }
}
