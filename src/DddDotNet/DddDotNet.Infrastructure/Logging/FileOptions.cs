using Serilog.Events;

namespace DddDotNet.Infrastructure.Logging;

public class FileOptions
{
    public LogEventLevel MinimumLogEventLevel { get; set; }
}
