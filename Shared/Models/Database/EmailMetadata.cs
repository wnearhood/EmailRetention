using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailRetention.Shared.Models.Database;

/// <summary>
/// Email metadata entity for Texas compliance logging
/// Contains all required metadata fields per State of Texas library requirements
/// </summary>
[Table("EmailMetadata")]
public class EmailMetadata
{
    /// <summary>
    /// Primary key - auto-generated
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Unique Message-ID header from email
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// Sender email address
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string SenderEmail { get; set; } = string.Empty;

    /// <summary>
    /// Sender display name
    /// </summary>
    [MaxLength(255)]
    public string? SenderDisplayName { get; set; }

    /// <summary>
    /// All recipients (To, CC, BCC) as JSON array
    /// </summary>
    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Recipients { get; set; } = string.Empty;

    /// <summary>
    /// Email subject line
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Date and time the email was sent
    /// </summary>
    [Required]
    public DateTime DateSent { get; set; }

    /// <summary>
    /// Date and time the email was deleted by retention policy
    /// </summary>
    [Required]
    public DateTime DateDeleted { get; set; }

    /// <summary>
    /// Primary mailbox owner where email resided
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string MailboxOwner { get; set; } = string.Empty;

    /// <summary>
    /// Message size in KB
    /// </summary>
    [Required]
    public long MessageSizeKB { get; set; }

    /// <summary>
    /// Original folder path/location in mailbox
    /// </summary>
    [MaxLength(500)]
    public string? FolderPath { get; set; }

    /// <summary>
    /// Retention policy name that caused deletion
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string RetentionPolicy { get; set; } = string.Empty;

    /// <summary>
    /// Reason for deletion (e.g., "Retention Policy", "Manual")
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string DeletionReason { get; set; } = string.Empty;

    /// <summary>
    /// Associated audit log entry ID for traceability
    /// </summary>
    [MaxLength(255)]
    public string? AuditLogId { get; set; }

    /// <summary>
    /// Whether email had attachments
    /// </summary>
    public bool HasAttachments { get; set; }

    /// <summary>
    /// List of attachment filenames as JSON array
    /// </summary>
    [Column(TypeName = "nvarchar(max)")]
    public string? AttachmentNames { get; set; }

    /// <summary>
    /// Email categories or flags as JSON array
    /// </summary>
    [Column(TypeName = "nvarchar(max)")]
    public string? Categories { get; set; }

    /// <summary>
    /// Importance level (Low, Normal, High)
    /// </summary>
    [MaxLength(20)]
    public string? Importance { get; set; }

    /// <summary>
    /// Whether email was read before deletion
    /// </summary>
    public bool? IsRead { get; set; }

    /// <summary>
    /// Internet message headers as JSON for additional compliance data
    /// </summary>
    [Column(TypeName = "nvarchar(max)")]
    public string? InternetMessageHeaders { get; set; }

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
    /// Hash of key fields for duplicate detection
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string RecordHash { get; set; } = string.Empty;
}