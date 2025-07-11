using EmailRetention.Shared.Services.Interfaces;
using EmailRetention.Shared.Models.Database;
using System.Collections.Concurrent;
using System.Text.Json;

namespace EmailRetention.Shared.Services;

/// <summary>
/// File-based logger service implementation
/// </summary>
public class FileLoggerService : ILoggerService, IDisposable
{
    private readonly string _logDirectory;
    private readonly string _logFileName;
    private readonly object _lockObject = new();
    private readonly ConcurrentQueue<string> _logQueue = new();
    private readonly Timer _flushTimer;
    private readonly IDatabaseService? _databaseService;
    private bool _disposed = false;

    public LogLevel LogLevel { get; set; } = LogLevel.Information;

    /// <summary>
    /// Initialize file logger service
    /// </summary>
    /// <param name="logDirectory">Directory for log files</param>
    /// <param name="databaseService">Optional database service for audit logging</param>
    public FileLoggerService(string logDirectory = "Logs", IDatabaseService? databaseService = null)
    {
        _logDirectory = logDirectory;
        _logFileName = $"EmailRetention_{DateTime.Now:yyyyMMdd}.log";
        _databaseService = databaseService;

        // Ensure log directory exists
        if (!Directory.Exists(_logDirectory))
        {
            Directory.CreateDirectory(_logDirectory);
        }

        // Start flush timer (flush every 5 seconds)
        _flushTimer = new Timer(FlushToFile, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
    }

    public void LogInformation(string message, string? component = null)
    {
        if (LogLevel <= LogLevel.Information)
        {
            WriteLog("INFO", message, component);
        }
    }

    public void LogWarning(string message, string? component = null)
    {
        if (LogLevel <= LogLevel.Warning)
        {
            WriteLog("WARN", message, component);
        }
    }

    public void LogError(string message, Exception? exception = null, string? component = null)
    {
        if (LogLevel <= LogLevel.Error)
        {
            var fullMessage = exception != null ? $"{message} | Exception: {exception}" : message;
            WriteLog("ERROR", fullMessage, component);
        }
    }

    public void LogDebug(string message, string? component = null)
    {
        if (LogLevel <= LogLevel.Debug)
        {
            WriteLog("DEBUG", message, component);
        }
    }

    public void LogCritical(string message, Exception exception, string? component = null)
    {
        if (LogLevel <= LogLevel.Critical)
        {
            var fullMessage = $"{message} | Critical Exception: {exception}";
            WriteLog("CRITICAL", fullMessage, component);
        }
    }

    public IDisposable BeginTimedOperation(string operationName, string? component = null)
    {
        return new TimedOperation(this, operationName, component);
    }

    public void LogAuditEvent(string operationType, string description, string result, 
        string? component = null, string? userId = null, int? recordsProcessed = null, 
        Dictionary<string, object>? metadata = null)
    {
        // Log to file
        var auditMessage = $"AUDIT | {operationType} | {result} | {description}";
        if (recordsProcessed.HasValue)
        {
            auditMessage += $" | Records: {recordsProcessed}";
        }
        WriteLog("AUDIT", auditMessage, component);

        // Log to database if available
        if (_databaseService != null)
        {
            Task.Run(async () =>
            {
                try
                {
                    var auditLog = new AuditLog
                    {
                        OperationType = operationType,
                        Description = description,
                        Result = result,
                        Component = component ?? "Unknown",
                        UserId = userId,
                        RecordsProcessed = recordsProcessed,
                        Metadata = metadata != null ? JsonSerializer.Serialize(metadata) : null,
                        Severity = result.Equals("Error", StringComparison.OrdinalIgnoreCase) ? "Error" : "Information",
                        MachineName = Environment.MachineName,
                        ApplicationVersion = GetType().Assembly.GetName().Version?.ToString()
                    };

                    await _databaseService.InsertAuditLogAsync(auditLog);
                }
                catch (Exception ex)
                {
                    // Fallback to file logging if database fails
                    WriteLog("ERROR", $"Failed to write audit log to database: {ex.Message}", "FileLoggerService");
                }
            });
        }
    }

    public async Task FlushAsync()
    {
        await Task.Run(() => FlushToFile(null));
    }

    private void WriteLog(string level, string message, string? component = null)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var logEntry = $"[{timestamp}] [{level}] {component ?? "General"} | {message}";
        
        _logQueue.Enqueue(logEntry);

        // Also write to console for immediate feedback
        var consoleColor = level switch
        {
            "ERROR" or "CRITICAL" => ConsoleColor.Red,
            "WARN" => ConsoleColor.Yellow,
            "DEBUG" => ConsoleColor.Gray,
            "AUDIT" => ConsoleColor.Green,
            _ => ConsoleColor.White
        };

        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(logEntry);
        Console.ForegroundColor = originalColor;
    }

    private void FlushToFile(object? state)
    {
        if (_logQueue.IsEmpty) return;

        try
        {
            var logFilePath = Path.Combine(_logDirectory, _logFileName);
            var entriesToWrite = new List<string>();

            // Dequeue all pending entries
            while (_logQueue.TryDequeue(out var entry))
            {
                entriesToWrite.Add(entry);
            }

            if (entriesToWrite.Count > 0)
            {
                lock (_lockObject)
                {
                    File.AppendAllLines(logFilePath, entriesToWrite);
                }
            }
        }
        catch (Exception ex)
        {
            // Fallback: write to console if file writing fails
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] FileLoggerService | Failed to write to log file: {ex.Message}");
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _flushTimer?.Dispose();
            FlushToFile(null); // Final flush
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Internal class for timed operations
    /// </summary>
    private class TimedOperation : IDisposable
    {
        private readonly FileLoggerService _logger;
        private readonly string _operationName;
        private readonly string? _component;
        private readonly DateTime _startTime;
        private bool _disposed = false;

        public TimedOperation(FileLoggerService logger, string operationName, string? component)
        {
            _logger = logger;
            _operationName = operationName;
            _component = component;
            _startTime = DateTime.Now;
            
            _logger.LogDebug($"Started operation: {_operationName}", _component);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                var duration = DateTime.Now - _startTime;
                _logger.LogDebug($"Completed operation: {_operationName} in {duration.TotalMilliseconds:F2}ms", _component);
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}