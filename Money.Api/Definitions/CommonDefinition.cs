﻿using Money.Api.Definitions.Base;
using Money.Common;

namespace Money.Api.Definitions;

public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new EnumerationConverterFactory());
            });

        builder.Services.AddLocalization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddResponseCaching();
        builder.Services.AddMemoryCache();
    }

    public override void ConfigureApplication(WebApplication app)
    {
        app.MapControllers();
        app.MapDefaultControllerRoute();
    }
}