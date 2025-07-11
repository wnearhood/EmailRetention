using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailRetention.Shared.Models.Database;

/// <summary>
/// Retention policy tracking entity for monitoring M365 policy status
/// </summary>
[Table("RetentionPolicy")]
public class RetentionPolicy
{
    /// <summary>
    /// Primary key - auto-generated
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// M365 retention policy name
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string PolicyName { get; set; } = string.Empty;

    /// <summary>
    /// M365 retention policy GUID
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string PolicyId { get; set; } = string.Empty;

    /// <summary>
    /// Policy description from M365
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Retention duration in days
    /// </summary>
    [Required]
    public int RetentionDurationDays { get; set; }

    /// <summary>
    /// Policy action (Delete, Retain, RetainAndDelete)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string PolicyAction { get; set; } = string.Empty;

    /// <summary>
    /// Whether the policy is currently enabled
    /// </summary>
    [Required]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Date the policy was created in M365
    /// </summary>
    [Required]
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Date the policy was last modified in M365
    /// </summary>
    public DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Last time we checked the policy status
    /// </summary>
    [Required]
    public DateTime LastChecked { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Locations where the policy is applied (Exchange, SharePoint, etc.)
    /// </summary>
    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string AppliedLocations { get; set; } = string.Empty;

    /// <summary>
    /// Number of mailboxes affected by this policy
    /// </summary>
    public int? AffectedMailboxCount { get; set; }

    /// <summary>
    /// Estimated number of items subject to this policy
    /// </summary>
    public long? EstimatedItemCount { get; set; }

    /// <summary>
    /// Policy mode (Enforce, TestWithNotifications, TestWithoutNotifications)
    /// </summary>
    [MaxLength(50)]
    public string? PolicyMode { get; set; }

    /// <summary>
    /// Advanced settings as JSON
    /// </summary>
    [Column(TypeName = "nvarchar(max)")]
    public string? AdvancedSettings { get; set; }

    /// <summary>
    /// Record creation timestamp
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Record last updated timestamp
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Whether this policy is actively monitored for compliance
    /// </summary>
    [Required]
    public bool IsMonitored { get; set; } = true;

    /// <summary>
    /// Texas-specific compliance status
    /// </summary>
    [MaxLength(50)]
    public string? ComplianceStatus { get; set; }

    /// <summary>
    /// Notes for compliance documentation
    /// </summary>
    [MaxLength(2000)]
    public string? ComplianceNotes { get; set; }
}