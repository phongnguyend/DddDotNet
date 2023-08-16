using DddDotNet.Infrastructure.Monitoring.AzureApplicationInsights;
using DddDotNet.Infrastructure.Monitoring.MiniProfiler;

namespace DddDotNet.Infrastructure.Monitoring;

public class MonitoringOptions
{
    public MiniProfilerOptions MiniProfiler { get; set; }

    public AzureApplicationInsightsOptions AzureApplicationInsights { get; set; }
}
