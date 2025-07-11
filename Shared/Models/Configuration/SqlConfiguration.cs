using System.ComponentModel.DataAnnotations;

namespace EmailRetention.Shared.Models.Configuration;

/// <summary>
/// SQL Server configuration for both local and Azure deployments
/// </summary>
public class SqlConfiguration
{
    /// <summary>
    /// SQL Server connection string
    /// </summary>
    [Required]
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Database name for email retention data
    /// </summary>
    [Required]
    public string DatabaseName { get; set; } = "EmailRetention";

    /// <summary>
    /// Schema name for email retention tables
    /// </summary>
    public string SchemaName { get; set; } = "dbo";

    /// <summary>
    /// Connection timeout in seconds
    /// </summary>
    public int ConnectionTimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Command timeout in seconds for long-running operations
    /// </summary>
    public int CommandTimeoutSeconds { get; set; } = 300;

    /// <summary>
    /// Enable SQL connection pooling
    /// </summary>
    public bool EnableConnectionPooling { get; set; } = true;

    /// <summary>
    /// Minimum pool size for connection pooling
    /// </summary>
    public int MinPoolSize { get; set; } = 5;

    /// <summary>
    /// Maximum pool size for connection pooling
    /// </summary>
    public int MaxPoolSize { get; set; } = 100;

    /// <summary>
    /// Whether to create database and tables if they don't exist
    /// </summary>
    public bool AutoCreateDatabase { get; set; } = true;

    /// <summary>
    /// Enable Entity Framework migrations
    /// </summary>
    public bool EnableMigrations { get; set; } = true;
}