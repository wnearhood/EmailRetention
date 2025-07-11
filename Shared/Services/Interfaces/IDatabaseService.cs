using EmailRetention.Shared.Models.Database;
using EmailRetention.Shared.Models.Configuration;

namespace EmailRetention.Shared.Services.Interfaces;

/// <summary>
/// Interface for database operations and entity management
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// Initialize the database service with configuration
    /// </summary>
    /// <param name="configuration">SQL configuration</param>
    Task InitializeAsync(SqlConfiguration configuration);

    /// <summary>
    /// Test the database connection
    /// </summary>
    /// <returns>True if connection is successful</returns>
    Task<bool> TestConnectionAsync();

    /// <summary>
    /// Create database and tables if they don't exist
    /// </summary>
    Task CreateDatabaseAsync();

    /// <summary>
    /// Run database migrations to latest version
    /// </summary>
    Task MigrateDatabaseAsync();

    /// <summary>
    /// Insert email metadata record
    /// </summary>
    /// <param name="emailMetadata">Email metadata to insert</param>
    /// <returns>Generated ID of the inserted record</returns>
    Task<int> InsertEmailMetadataAsync(EmailMetadata emailMetadata);

    /// <summary>
    /// Insert multiple email metadata records in a batch
    /// </summary>
    /// <param name="emailMetadataList">List of email metadata to insert</param>
    /// <returns>Number of records inserted</returns>
    Task<int> InsertEmailMetadataBatchAsync(IEnumerable<EmailMetadata> emailMetadataList);

    /// <summary>
    /// Check if email metadata already exists (duplicate detection)
    /// </summary>
    /// <param name="messageId">Message ID to check</param>
    /// <param name="mailboxOwner">Mailbox owner</param>
    /// <returns>True if record already exists</returns>
    Task<bool> EmailMetadataExistsAsync(string messageId, string mailboxOwner);

    /// <summary>
    /// Get email metadata by ID
    /// </summary>
    /// <param name="id">Record ID</param>
    /// <returns>Email metadata or null if not found</returns>
    Task<EmailMetadata?> GetEmailMetadataAsync(int id);

    /// <summary>
    /// Search email metadata by criteria
    /// </summary>
    /// <param name="startDate">Start date filter</param>
    /// <param name="endDate">End date filter</param>
    /// <param name="mailboxOwner">Optional mailbox owner filter</param>
    /// <param name="retentionPolicy">Optional retention policy filter</param>
    /// <param name="pageSize">Number of records per page</param>
    /// <param name="pageNumber">Page number (1-based)</param>
    /// <returns>Paginated email metadata results</returns>
    Task<(IEnumerable<EmailMetadata> Records, int TotalCount)> SearchEmailMetadataAsync(
        DateTime? startDate = null, DateTime? endDate = null, string? mailboxOwner = null, 
        string? retentionPolicy = null, int pageSize = 100, int pageNumber = 1);

    /// <summary>
    /// Insert audit log entry
    /// </summary>
    /// <param name="auditLog">Audit log entry to insert</param>
    /// <returns>Generated ID of the inserted record</returns>
    Task<int> InsertAuditLogAsync(AuditLog auditLog);

    /// <summary>
    /// Get audit log entries by criteria
    /// </summary>
    /// <param name="startDate">Start date filter</param>
    /// <param name="endDate">End date filter</param>
    /// <param name="operationType">Optional operation type filter</param>
    /// <param name="component">Optional component filter</param>
    /// <param name="severity">Optional severity filter</param>
    /// <param name="pageSize">Number of records per page</param>
    /// <param name="pageNumber">Page number (1-based)</param>
    /// <returns>Paginated audit log results</returns>
    Task<(IEnumerable<AuditLog> Records, int TotalCount)> GetAuditLogsAsync(
        DateTime? startDate = null, DateTime? endDate = null, string? operationType = null,
        string? component = null, string? severity = null, int pageSize = 100, int pageNumber = 1);

    /// <summary>
    /// Insert or update retention policy information
    /// </summary>
    /// <param name="retentionPolicy">Retention policy to upsert</param>
    /// <returns>Generated or existing ID</returns>
    Task<int> UpsertRetentionPolicyAsync(RetentionPolicy retentionPolicy);

    /// <summary>
    /// Get retention policy by name
    /// </summary>
    /// <param name="policyName">Policy name</param>
    /// <returns>Retention policy or null if not found</returns>
    Task<RetentionPolicy?> GetRetentionPolicyAsync(string policyName);

    /// <summary>
    /// Get all monitored retention policies
    /// </summary>
    /// <returns>List of monitored retention policies</returns>
    Task<IEnumerable<RetentionPolicy>> GetMonitoredRetentionPoliciesAsync();

    /// <summary>
    /// Generate compliance report for a date range
    /// </summary>
    /// <param name="startDate">Start date for report</param>
    /// <param name="endDate">End date for report</param>
    /// <returns>Compliance report data</returns>
    Task<object> GenerateComplianceReportAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Get database statistics and health information
    /// </summary>
    /// <returns>Database statistics</returns>
    Task<object> GetDatabaseStatisticsAsync();

    /// <summary>
    /// Cleanup old audit log entries based on retention settings
    /// </summary>
    /// <param name="retentionDays">Number of days to retain audit logs</param>
    /// <returns>Number of records deleted</returns>
    Task<int> CleanupAuditLogsAsync(int retentionDays = 365);

    /// <summary>
    /// Execute database maintenance tasks
    /// </summary>
    Task PerformMaintenanceAsync();

    /// <summary>
    /// Backup database (if supported)
    /// </summary>
    /// <param name="backupPath">Path for backup file</param>
    Task BackupDatabaseAsync(string backupPath);
}