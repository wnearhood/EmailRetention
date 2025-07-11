namespace EmailRetention.Shared.Services.Interfaces;

/// <summary>
/// Interface for application logging services
/// </summary>
public interface ILoggerService
{
    /// <summary>
    /// Log an informational message
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="component">Component or class generating the log</param>
    void LogInformation(string message, string? component = null);

    /// <summary>
    /// Log a warning message
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="component">Component or class generating the log</param>
    void LogWarning(string message, string? component = null);

    /// <summary>
    /// Log an error message
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Optional exception details</param>
    /// <param name="component">Component or class generating the log</param>
    void LogError(string message, Exception? exception = null, string? component = null);

    /// <summary>
    /// Log a debug message (only in debug builds or when detailed logging enabled)
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="component">Component or class generating the log</param>
    void LogDebug(string message, string? component = null);

    /// <summary>
    /// Log a critical error that requires immediate attention
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Exception details</param>
    /// <param name="component">Component or class generating the log</param>
    void LogCritical(string message, Exception exception, string? component = null);

    /// <summary>
    /// Start a timed operation for performance logging
    /// </summary>
    /// <param name="operationName">Name of the operation</param>
    /// <param name="component">Component performing the operation</param>
    /// <returns>Disposable object that logs completion time when disposed</returns>
    IDisposable BeginTimedOperation(string operationName, string? component = null);

    /// <summary>
    /// Log an audit event for compliance tracking
    /// </summary>
    /// <param name="operationType">Type of operation</param>
    /// <param name="description">Description of the operation</param>
    /// <param name="result">Result of the operation</param>
    /// <param name="component">Component performing the operation</param>
    /// <param name="userId">User or service account</param>
    /// <param name="recordsProcessed">Number of records processed</param>
    /// <param name="metadata">Additional metadata as key-value pairs</param>
    void LogAuditEvent(string operationType, string description, string result, 
        string? component = null, string? userId = null, int? recordsProcessed = null, 
        Dictionary<string, object>? metadata = null);

    /// <summary>
    /// Flush any pending log entries to storage
    /// </summary>
    Task FlushAsync();

    /// <summary>
    /// Get the current log level threshold
    /// </summary>
    LogLevel LogLevel { get; set; }
}

/// <summary>
/// Log level enumeration
/// </summary>
public enum LogLevel
{
    Debug = 0,
    Information = 1,
    Warning = 2,
    Error = 3,
    Critical = 4,
    None = 5
}