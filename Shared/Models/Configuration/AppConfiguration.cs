using System.ComponentModel.DataAnnotations;

namespace EmailRetention.Shared.Models.Configuration;

/// <summary>
/// Main application configuration containing all deployment and authentication settings
/// </summary>
public class AppConfiguration
{
    /// <summary>
    /// Azure AD Application ID for Graph API authentication
    /// </summary>
    [Required]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// Client secret for application authentication
    /// </summary>
    [Required]
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Azure AD Tenant ID
    /// </summary>
    [Required]
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Deployment type: Local or Azure
    /// </summary>
    [Required]
    public DeploymentType DeploymentType { get; set; }

    /// <summary>
    /// SQL Server configuration settings
    /// </summary>
    [Required]
    public SqlConfiguration SqlConfiguration { get; set; } = new();

    /// <summary>
    /// Azure-specific configuration (only used when DeploymentType is Azure)
    /// </summary>
    public AzureConfiguration? AzureConfiguration { get; set; }

    /// <summary>
    /// General application settings
    /// </summary>
    public AppSettings AppSettings { get; set; } = new();
}

/// <summary>
/// Deployment type enumeration
/// </summary>
public enum DeploymentType
{
    Local,
    Azure
}

/// <summary>
/// General application settings
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Batch size for email processing operations
    /// </summary>
    public int BatchSize { get; set; } = 100;

    /// <summary>
    /// Number of days to look back for email metadata extraction
    /// </summary>
    public int LookbackDays { get; set; } = 7;

    /// <summary>
    /// Enable detailed logging
    /// </summary>
    public bool EnableDetailedLogging { get; set; } = true;

    /// <summary>
    /// Maximum number of retry attempts for failed operations
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Retention policy name to monitor
    /// </summary>
    public string RetentionPolicyName { get; set; } = "TX-Email-Retention-5Year";
}