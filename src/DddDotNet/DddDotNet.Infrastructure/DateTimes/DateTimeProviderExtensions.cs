﻿using DddDotNet.CrossCuttingConcerns.DateTimes;
using DddDotNet.Infrastructure.DateTimes;

namespace Microsoft.Extensions.DependencyInjection;

public static class DateTimeProviderExtensions
{
    public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
    {
        _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
