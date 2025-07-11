using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using EmailRetention.Shared.Models.Configuration;

namespace EmailRetention.Shared.Utilities;

/// <summary>
/// Validation helper methods for configuration and data validation
/// </summary>
public static class ValidationHelpers
{
    /// <summary>
    /// Validate email address format
    /// </summary>
    /// <param name="email">Email address to validate</param>
    /// <returns>True if email is valid</returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validate SQL Server connection string format
    /// </summary>
    /// <param name="connectionString">Connection string to validate</param>
    /// <returns>True if connection string appears valid</returns>
    public static bool IsValidConnectionString(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            return false;

        // Check for required components
        var lowerConnectionString = connectionString.ToLowerInvariant();
        
        // Must have Server and Database
        if (!lowerConnectionString.Contains("server=") && !lowerConnectionString.Contains("data source="))
            return false;

        if (!lowerConnectionString.Contains("database=") && !lowerConnectionString.Contains("initial catalog="))
            return false;

        return true;
    }

    /// <summary>
    /// Validate Azure AD Application ID (GUID format)
    /// </summary>
    /// <param name="appId">Application ID to validate</param>
    /// <returns>True if App ID is valid GUID</returns>
    public static bool IsValidAppId(string appId)
    {
        return Guid.TryParse(appId, out _);
    }

    /// <summary>
    /// Validate Azure AD Tenant ID (GUID format)
    /// </summary>
    /// <param name="tenantId">Tenant ID to validate</param>
    /// <returns>True if Tenant ID is valid GUID</returns>
    public static bool IsValidTenantId(string tenantId)
    {
        return Guid.TryParse(tenantId, out _);
    }

    /// <summary>
    /// Validate client secret format and strength
    /// </summary>
    /// <param name="clientSecret">Client secret to validate</param>
    /// <returns>True if client secret appears valid</returns>
    public static bool IsValidClientSecret(string clientSecret)
    {
        if (string.IsNullOrWhiteSpace(clientSecret))
            return false;

        // Client secrets should be at least 32 characters
        if (clientSecret.Length < 32)
            return false;

        // Should contain alphanumeric characters and special characters
        var hasLetter = clientSecret.Any(char.IsLetter);
        var hasDigit = clientSecret.Any(char.IsDigit);
        
        return hasLetter && hasDigit;
    }

    /// <summary>
    /// Validate URL format
    /// </summary>
    /// <param name="url">URL to validate</param>
    /// <returns>True if URL is valid</returns>
    public static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// Validate application configuration completeness
    /// </summary>
    /// <param name="configuration">Configuration to validate</param>
    /// <returns>Validation result with error messages</returns>
    public static ValidationResult ValidateConfiguration(AppConfiguration configuration)
    {
        var errors = new List<string>();

        // Required fields
        if (string.IsNullOrWhiteSpace(configuration.AppId))
            errors.Add("App ID is required");
        else if (!IsValidAppId(configuration.AppId))
            errors.Add("App ID must be a valid GUID");

        if (string.IsNullOrWhiteSpace(configuration.ClientSecret))
            errors.Add("Client Secret is required");
        else if (!IsValidClientSecret(configuration.ClientSecret))
            errors.Add("Client Secret appears to be invalid or too weak");

        if (string.IsNullOrWhiteSpace(configuration.TenantId))
            errors.Add("Tenant ID is required");
        else if (!IsValidTenantId(configuration.TenantId))
            errors.Add("Tenant ID must be a valid GUID");

        // SQL Configuration
        if (configuration.SqlConfiguration == null)
            errors.Add("SQL Configuration is required");
        else
        {
            if (string.IsNullOrWhiteSpace(configuration.SqlConfiguration.ConnectionString))
                errors.Add("SQL Connection String is required");
            else if (!IsValidConnectionString(configuration.SqlConfiguration.ConnectionString))
                errors.Add("SQL Connection String appears to be invalid");

            if (string.IsNullOrWhiteSpace(configuration.SqlConfiguration.DatabaseName))
                errors.Add("Database Name is required");

            if (configuration.SqlConfiguration.ConnectionTimeoutSeconds <= 0)
                errors.Add("Connection Timeout must be greater than 0");

            if (configuration.SqlConfiguration.CommandTimeoutSeconds <= 0)
                errors.Add("Command Timeout must be greater than 0");
        }

        // Azure Configuration (if Azure deployment)
        if (configuration.DeploymentType == DeploymentType.Azure && configuration.AzureConfiguration != null)
        {
            if (configuration.AzureConfiguration.EnablePurviewStreaming)
            {
                if (string.IsNullOrWhiteSpace(configuration.AzureConfiguration.EventHubConnectionString))
                    errors.Add("Event Hub Connection String is required when Purview streaming is enabled");

                if (string.IsNullOrWhiteSpace(configuration.AzureConfiguration.EventHubName))
                    errors.Add("Event Hub Name is required when Purview streaming is enabled");
            }

            if (!string.IsNullOrWhiteSpace(configuration.AzureConfiguration.KeyVaultUrl) && 
                !IsValidUrl(configuration.AzureConfiguration.KeyVaultUrl))
                errors.Add("Key Vault URL must be a valid URL");
        }

        // App Settings validation
        if (configuration.AppSettings.BatchSize <= 0 || configuration.AppSettings.BatchSize > 1000)
            errors.Add("Batch Size must be between 1 and 1000");

        if (configuration.AppSettings.LookbackDays <= 0 || configuration.AppSettings.LookbackDays > 365)
            errors.Add("Lookback Days must be between 1 and 365");

        if (configuration.AppSettings.MaxRetryAttempts <= 0 || configuration.AppSettings.MaxRetryAttempts > 10)
            errors.Add("Max Retry Attempts must be between 1 and 10");

        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }

    /// <summary>
    /// Sanitize string input for database storage
    /// </summary>
    /// <param name="input">Input string to sanitize</param>
    /// <param name="maxLength">Maximum allowed length</param>
    /// <returns>Sanitized string</returns>
    public static string SanitizeString(string? input, int maxLength = 1000)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Remove control characters except CR, LF, and Tab
        var sanitized = Regex.Replace(input, @"[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]", string.Empty);

        // Truncate if too long
        if (sanitized.Length > maxLength)
            sanitized = sanitized.Substring(0, maxLength);

        return sanitized.Trim();
    }

    /// <summary>
    /// Generate a hash for duplicate detection
    /// </summary>
    /// <param name="values">Values to hash</param>
    /// <returns>SHA256 hash string</returns>
    public static string GenerateHash(params string[] values)
    {
        var combined = string.Join("|", values.Where(v => !string.IsNullOrEmpty(v)));
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combined));
        return Convert.ToHexString(hash);
    }

    /// <summary>
    /// Validate file path for security
    /// </summary>
    /// <param name="filePath">File path to validate</param>
    /// <returns>True if path is safe</returns>
    public static bool IsValidFilePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            // Check for invalid characters
            var invalidChars = Path.GetInvalidPathChars();
            if (filePath.Any(c => invalidChars.Contains(c)))
                return false;

            // Check for path traversal attempts
            if (filePath.Contains("..") || filePath.Contains("~"))
                return false;

            // Ensure it's a valid path format
            Path.GetFullPath(filePath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validate Message ID format (RFC 2822)
    /// </summary>
    /// <param name="messageId">Message ID to validate</param>
    /// <returns>True if Message ID appears valid</returns>
    public static bool IsValidMessageId(string messageId)
    {
        if (string.IsNullOrWhiteSpace(messageId))
            return false;

        // Basic RFC 2822 Message-ID format validation
        // Should be in format: <local-part@domain-part>
        var messageIdPattern = @"^<[^<>@\s]+@[^<>@\s]+\.[^<>@\s]+>$";
        return Regex.IsMatch(messageId, messageIdPattern);
    }
}

/// <summary>
/// Validation result container
/// </summary>
public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();

    public string GetErrorSummary()
    {
        return string.Join(Environment.NewLine, Errors);
    }

    public string GetWarningSummary()
    {
        return string.Join(Environment.NewLine, Warnings);
    }
}