using Microsoft.Graph;
using EmailRetention.Shared.Models.Configuration;

namespace EmailRetention.Shared.Services.Interfaces;

/// <summary>
/// Interface for Microsoft Graph API operations
/// </summary>
public interface IGraphService
{
    /// <summary>
    /// Initialize the Graph service with application configuration
    /// </summary>
    /// <param name="configuration">Application configuration</param>
    Task InitializeAsync(AppConfiguration configuration);

    /// <summary>
    /// Test the Graph API connection and permissions
    /// </summary>
    /// <returns>True if connection is successful and permissions are adequate</returns>
    Task<bool> TestConnectionAsync();

    /// <summary>
    /// Get the current authenticated user or service principal information
    /// </summary>
    /// <returns>User information or null if not authenticated</returns>
    Task<User?> GetCurrentUserAsync();

    /// <summary>
    /// Get all mailboxes in the organization
    /// </summary>
    /// <returns>List of mailbox identities</returns>
    Task<IEnumerable<string>> GetAllMailboxesAsync();

    /// <summary>
    /// Get email messages from a specific mailbox within a date range
    /// </summary>
    /// <param name="mailboxId">Mailbox identifier (email address or ID)</param>
    /// <param name="startDate">Start date for message search</param>
    /// <param name="endDate">End date for message search</param>
    /// <param name="batchSize">Number of messages to retrieve per batch</param>
    /// <returns>Email messages matching criteria</returns>
    Task<IEnumerable<Message>> GetEmailsAsync(string mailboxId, DateTime startDate, DateTime endDate, int batchSize = 100);

    /// <summary>
    /// Get detailed metadata for a specific email message
    /// </summary>
    /// <param name="mailboxId">Mailbox identifier</param>
    /// <param name="messageId">Message identifier</param>
    /// <returns>Complete message metadata</returns>
    Task<Message?> GetEmailMetadataAsync(string mailboxId, string messageId);

    /// <summary>
    /// Get emails that are nearing their retention policy deletion date
    /// </summary>
    /// <param name="daysBeforeDeletion">Number of days before deletion to search</param>
    /// <param name="batchSize">Number of messages to retrieve per batch</param>
    /// <returns>Email messages approaching deletion</returns>
    Task<IEnumerable<Message>> GetEmailsNearingDeletionAsync(int daysBeforeDeletion = 7, int batchSize = 100);

    /// <summary>
    /// Search for emails using advanced query parameters
    /// </summary>
    /// <param name="query">OData query string</param>
    /// <param name="mailboxId">Optional specific mailbox to search</param>
    /// <param name="batchSize">Number of messages to retrieve per batch</param>
    /// <returns>Email messages matching query</returns>
    Task<IEnumerable<Message>> SearchEmailsAsync(string query, string? mailboxId = null, int batchSize = 100);

    /// <summary>
    /// Get retention policies applied to Exchange Online
    /// </summary>
    /// <returns>Retention policy information</returns>
    Task<IEnumerable<object>> GetRetentionPoliciesAsync();

    /// <summary>
    /// Check if a specific retention policy is active
    /// </summary>
    /// <param name="policyName">Name of the retention policy</param>
    /// <returns>True if policy is active</returns>
    Task<bool> IsRetentionPolicyActiveAsync(string policyName);

    /// <summary>
    /// Get audit log entries for retention policy activities
    /// </summary>
    /// <param name="startDate">Start date for audit search</param>
    /// <param name="endDate">End date for audit search</param>
    /// <param name="operationType">Optional operation type filter</param>
    /// <returns>Audit log entries</returns>
    Task<IEnumerable<object>> GetAuditLogsAsync(DateTime startDate, DateTime endDate, string? operationType = null);

    /// <summary>
    /// Refresh the authentication token
    /// </summary>
    Task RefreshTokenAsync();

    /// <summary>
    /// Get the current authentication status
    /// </summary>
    /// <returns>True if authenticated and token is valid</returns>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Get the token expiration time
    /// </summary>
    DateTime? TokenExpiresAt { get; }
}