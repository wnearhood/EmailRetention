namespace EmailRetention.Shared.Utilities;

/// <summary>
/// Application constants and default values
/// </summary>
public static class Constants
{
    /// <summary>
    /// Application information
    /// </summary>
    public static class Application
    {
        public const string Name = "Texas Municipality Email Retention";
        public const string Version = "1.0.0";
        public const string Description = "Email retention compliance solution for Texas municipalities";
        public const string Publisher = "Texas Municipality IT Department";
    }

    /// <summary>
    /// Configuration keys and default values
    /// </summary>
    public static class Configuration
    {
        public const string DefaultConfigFileName = "EmailRetentionConfig.json";
        public const string DefaultLogDirectory = "Logs";
        public const string DefaultDatabaseName = "EmailRetention";
        public const string DefaultSchemaName = "dbo";
        public const int DefaultBatchSize = 100;
        public const int DefaultLookbackDays = 7;
        public const int DefaultRetryAttempts = 3;
        public const int DefaultConnectionTimeoutSeconds = 30;
        public const int DefaultCommandTimeoutSeconds = 300;
    }

    /// <summary>
    /// Microsoft Graph API constants
    /// </summary>
    public static class GraphApi
    {
        public const string BaseUrl = "https://graph.microsoft.com/v1.0";
        public const string AuthorityUrl = "https://login.microsoftonline.com";
        public const string DefaultScope = "https://graph.microsoft.com/.default";
        
        // Required permissions
        public static readonly string[] RequiredScopes = 
        {
            "https://graph.microsoft.com/Mail.Read",
            "https://graph.microsoft.com/Mail.ReadBasic.All",
            "https://graph.microsoft.com/Files.ReadWrite.All"
        };

        // Application permissions
        public static readonly Dictionary<string, string> ApplicationPermissions = new()
        {
            { "Microsoft Graph", "00000003-0000-0000-c000-000000000000" },
            { "Exchange Online", "00000002-0000-0ff1-ce00-000000000000" },
            { "SharePoint", "00000003-0000-0ff1-ce00-000000000000" }
        };

        // Permission IDs
        public static readonly Dictionary<string, string> PermissionIds = new()
        {
            { "Mail.Read", "810c84a8-4a9e-49e6-bf7d-12d183f40d01" },
            { "Mail.ReadBasic.All", "693c5e45-0940-467d-9b8a-1022fb9d42ef" },
            { "Files.ReadWrite.All", "75359482-378d-4052-8f01-80520e7db3cd" },
            { "Directory.ReadWrite.All", "19dbc75e-c2e2-444c-a770-ec69d8559fc7" },
            { "Exchange.full_access_as_app", "dc890d15-9560-4a4c-9b7f-a736ec74ec40" }
        };
    }

    /// <summary>
    /// Texas State compliance constants
    /// </summary>
    public static class TexasCompliance
    {
        public const string StateLibraryRequirement = "Texas State Library and Archives Commission";
        public const string PublicInformationAct = "Texas Public Information Act";
        public const int RequiredRetentionYears = 5;
        public const int RequiredRetentionDays = 1825; // 5 years
        public const string PermanentRetentionDescription = "Administrative information retained in perpetuity per Texas State Library requirements";
        
        // Required metadata fields per Texas State Library
        public static readonly string[] RequiredMetadataFields = 
        {
            "MessageID", "SenderEmail", "Recipients", "Subject", "DateSent", 
            "DateDeleted", "MailboxOwner", "MessageSize", "FolderPath", 
            "RetentionPolicy", "AuditLogID"
        };
    }

    /// <summary>
    /// Database constants
    /// </summary>
    public static class Database
    {
        public const string EmailMetadataTable = "EmailMetadata";
        public const string AuditLogTable = "AuditLog";
        public const string RetentionPolicyTable = "RetentionPolicy";
        
        // Connection string templates
        public const string LocalSqlTemplate = "Server={0};Database={1};Integrated Security=true;TrustServerCertificate=true;";
        public const string AzureSqlTemplate = "Server={0};Database={1};Authentication=Active Directory Default;TrustServerCertificate=true;";
        public const string AzureSqlWithCredsTemplate = "Server={0};Database={1};User ID={2};Password={3};TrustServerCertificate=true;";
    }

    /// <summary>
    /// File and directory constants
    /// </summary>
    public static class Files
    {
        public const string LogFilePrefix = "EmailRetention_";
        public const string LogFileExtension = ".log";
        public const string ConfigFileExtension = ".json";
        public const string BackupFileExtension = ".bak";
        public const string CertificateFileExtension = ".pfx";
        
        // File size limits
        public const long MaxLogFileSizeMB = 100;
        public const long MaxConfigFileSizeKB = 500;
    }

    /// <summary>
    /// Performance and timing constants
    /// </summary>
    public static class Performance
    {
        public const int DefaultPageSize = 100;
        public const int MaxPageSize = 1000;
        public const int DelayBetweenBatchesMs = 1000;
        public const int TokenRefreshBufferMinutes = 5;
        public const int MaxConcurrentOperations = 5;
        public const int DefaultTimeoutMinutes = 30;
    }

    /// <summary>
    /// Error handling constants
    /// </summary>
    public static class ErrorHandling
    {
        public const int MaxRetryAttempts = 3;
        public const int BaseRetryDelayMs = 1000;
        public const int MaxRetryDelayMs = 30000;
        public const double RetryBackoffMultiplier = 2.0;
        
        // Known error codes
        public static readonly Dictionary<string, string> KnownErrorCodes = new()
        {
            { "429", "Rate limit exceeded - throttling required" },
            { "401", "Authentication failed - token expired or invalid" },
            { "403", "Insufficient permissions" },
            { "404", "Resource not found" },
            { "500", "Internal server error" }
        };
    }

    /// <summary>
    /// Logging constants
    /// </summary>
    public static class Logging
    {
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public const string LogEntryFormat = "[{0}] [{1}] {2} | {3}";
        public const int MaxLogEntryLength = 10000;
        public const int FlushIntervalSeconds = 5;
        public const int MaxQueueSize = 1000;
    }
}