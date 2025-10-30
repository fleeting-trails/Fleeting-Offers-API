using Serilog;

public sealed class LoggerSettings
{
    public LoggerSettings(WebApplicationBuilder builder)
    {
        var logDir = builder.Configuration["LoggerSettings:LogDirPath"] ?? "/app/Logs";
        Directory.CreateDirectory(logDir); // ensure path exists

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                Path.Combine(logDir, "app-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                fileSizeLimitBytes: 10_000_000,   // ~10 MB per file
                rollOnFileSizeLimit: true,
                shared: true)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger, dispose: true);
    }
}