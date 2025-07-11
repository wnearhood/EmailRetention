using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailRetention.Shared.Models.Database;

/// <summary>
/// Audit log entity for tracking system operations and compliance events
/// </summary>
[Table("AuditLog")]
public class AuditLog
{
    /// <summary>
    /// Primary key - auto-generated
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Timestamp of the logged event
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Type of operation (e.g., "MetadataExtraction", "PolicyCheck", "Authentication")
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string OperationType { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the operation
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Result of the operation (Success, Warning, Error)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Result { get; set; } = string.Empty;

    /// <summary>
    /// Application component that generated the log entry
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// User or service account that performed the operation
    /// </summary>
    [MaxLength(255)]
    public string? UserId { get; set; }

    /// <summary>
    /// Session or correlation ID for tracking related operations
    /// </summary>
    [MaxLength(100)]
    public string? SessionId { get; set; }

    /// <summary>
    /// Number of records processed (for batch operations)
    /// </summary>
    public int? RecordsProcessed { get; set; }

    /// <summary>
    /// Duration of the operation in milliseconds
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Error message or exception details (if Result is Error)
    /// </summary>
    [Column(TypeName = "nvarchar(max)")]
    public string? ErrorDetails { get; set; }

    /// <summary>
    /// Additional metadata as JSON
    /// </summary>
    [Column(TypeName = "nvarchar(max)")]
    public string? Metadata { get; set; }

    /// <summary>
    /// Severity level for filtering and alerting
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Severity { get; set; } = "Information";

    /// <summary>
    /// Machine/server name where operation occurred
    /// </summary>
    [MaxLength(100)]
    public string? MachineName { get; set; }

    /// <summary>
    /// IP address of the client or server
    /// </summary>
    [MaxLength(45)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Version of the application that generated the log
    /// </summary>
    [MaxLength(20)]
    public string? ApplicationVersion { get; set; }
}

/// <summary>
/// Audit log operation types constants
/// </summary>
public static class AuditOperationType
{
    public const string Authentication = "Authentication";
    public const string MetadataExtraction = "MetadataExtraction";
    public const string PolicyCheck = "PolicyCheck";
    public const string DatabaseOperation = "DatabaseOperation";
    public const string ConfigurationChange = "ConfigurationChange";
    public const string RetentionPolicyExecution = "RetentionPolicyExecution";
    public const string ComplianceReport = "ComplianceReport";
    public const string SystemStartup = "SystemStartup";
    public const string SystemShutdown = "SystemShutdown";
    public const string ErrorRecovery = "ErrorRecovery";
}

/// <summary>
/// Audit log result types constants
/// </summary>
public static class AuditResult
{
    public const string Success = "Success";
    public const string Warning = "Warning";
    public const string Error = "Error";
    public const string Information = "Information";
}

/// <summary>
/// Audit log severity levels constants
/// </summary>
public static class AuditSeverity
{
    public const string Critical = "Critical";
    public const string Error = "Error";
    public const string Warning = "Warning";
    public const string Information = "Information";
    public const string Debug = "Debug";
}