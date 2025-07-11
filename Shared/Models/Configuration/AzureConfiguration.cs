using System.ComponentModel.DataAnnotations;

namespace EmailRetention.Shared.Models.Configuration;

/// <summary>
/// Azure-specific configuration settings
/// </summary>
public class AzureConfiguration
{
    /// <summary>
    /// Enable Microsoft Purview audit log streaming
    /// </summary>
    public bool EnablePurviewStreaming { get; set; } = false;

    /// <summary>
    /// Azure Event Hub connection string for Purview streaming
    /// </summary>
    public string? EventHubConnectionString { get; set; }

    /// <summary>
    /// Azure Event Hub name for Purview data
    /// </summary>
    public string? EventHubName { get; set; }

    /// <summary>
    /// Azure Storage Account for long-term archival
    /// </summary>
    public string? StorageAccountConnectionString { get; set; }

    /// <summary>
    /// Azure Storage Container for email metadata archives
    /// </summary>
    public string? StorageContainerName { get; set; } = "email-retention-archive";

    /// <summary>
    /// Azure Key Vault URL for secure credential storage
    /// </summary>
    public string? KeyVaultUrl { get; set; }

    /// <summary>
    /// Azure Application Insights instrumentation key for monitoring
    /// </summary>
    public string? ApplicationInsightsKey { get; set; }

    /// <summary>
    /// Azure region for compliance and data residency
    /// </summary>
    public string AzureRegion { get; set; } = "South Central US";

    /// <summary>
    /// Enable Azure Active Directory authentication for SQL
    /// </summary>
    public bool UseAzureAdAuthentication { get; set; } = true;

    /// <summary>
    /// Azure resource group name for resource management
    /// </summary>
    public string? ResourceGroupName { get; set; }

    /// <summary>
    /// Tags for Azure resource compliance and billing
    /// </summary>
    public Dictionary<string, string> ResourceTags { get; set; } = new()
    {
        { "Purpose", "EmailRetentionCompliance" },
        { "Owner", "Municipality" },
        { "Environment", "Production" },
        { "Compliance", "TexasStateLibrary" }
    };
}