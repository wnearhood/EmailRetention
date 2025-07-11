using EmailRetention.Shared.Services.Interfaces;

namespace EmailRetention.Setup;

/// <summary>
/// Main form for the Email Retention Setup wizard
/// </summary>
public partial class MainForm : Form
{
    private readonly ILoggerService _logger;
    private int _currentStep = 1;
    private const int _totalSteps = 6;

    /// <summary>
    /// Initialize the main form
    /// </summary>
    /// <param name="logger">Logging service instance</param>
    public MainForm(ILoggerService logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        InitializeComponent();
        InitializeForm();
        LoadWelcomeStep();
    }

    /// <summary>
    /// Initialize form properties and event handlers
    /// </summary>
    private void InitializeForm()
    {
        _logger.LogInformation("Initializing main form");

        // Set form properties
        this.Icon = SystemIcons.Application;
        
        // Wire up event handlers
        this.buttonBack.Click += ButtonBack_Click;
        this.buttonNext.Click += ButtonNext_Click;
        this.buttonCancel.Click += ButtonCancel_Click;
        this.FormClosing += MainForm_FormClosing;

        // Update UI state
        UpdateNavigationButtons();
        UpdateProgress();
        
        _logger.LogInformation("Main form initialized successfully");
    }

    /// <summary>
    /// Load the welcome step content
    /// </summary>
    private void LoadWelcomeStep()
    {
        _logger.LogInformation("Loading welcome step");

        // Clear main panel
        panelMain.Controls.Clear();

        // Create welcome content
        var welcomePanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(0)
        };

        var titleLabel = new Label
        {
            Text = "Welcome to Email Retention Setup",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            ForeColor = Color.FromArgb(51, 51, 51),
            AutoSize = true,
            Location = new Point(0, 20)
        };

        var descriptionLabel = new Label
        {
            Text = "This wizard will help you configure email retention policies for your Texas municipality.\n\n" +
                   "The setup process includes:\n" +
                   "• Microsoft Graph authentication configuration\n" +
                   "• Azure AD application registration\n" +
                   "• Deployment type selection (Local or Azure)\n" +
                   "• SQL Server connection testing\n" +
                   "• Configuration export for operations\n\n" +
                   "Before continuing, please ensure you have:\n" +
                   "• Global Administrator access to your Microsoft 365 tenant\n" +
                   "• Access to SQL Server (local or Azure)\n" +
                   "• Network connectivity to Microsoft services",
            Font = new Font("Segoe UI", 9F),
            ForeColor = Color.FromArgb(68, 68, 68),
            Location = new Point(0, 60),
            Size = new Size(700, 280),
            AutoSize = false
        };

        var readyLabel = new Label
        {
            Text = "Click 'Next' when you are ready to begin the configuration process.",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 120, 215),
            AutoSize = true,
            Location = new Point(0, 360)
        };

        welcomePanel.Controls.Add(titleLabel);
        welcomePanel.Controls.Add(descriptionLabel);
        welcomePanel.Controls.Add(readyLabel);
        panelMain.Controls.Add(welcomePanel);

        SetStatus("Ready to begin setup process");
        _logger.LogInformation("Welcome step loaded successfully");
    }

    /// <summary>
    /// Handle Back button click
    /// </summary>
    private void ButtonBack_Click(object? sender, EventArgs e)
    {
        _logger.LogInformation($"Back button clicked from step {_currentStep}");

        if (_currentStep > 1)
        {
            _currentStep--;
            NavigateToStep(_currentStep);
        }
    }

    /// <summary>
    /// Handle Next button click
    /// </summary>
    private void ButtonNext_Click(object? sender, EventArgs e)
    {
        _logger.LogInformation($"Next button clicked from step {_currentStep}");

        if (ValidateCurrentStep())
        {
            if (_currentStep < _totalSteps)
            {
                _currentStep++;
                NavigateToStep(_currentStep);
            }
            else
            {
                CompleteSetup();
            }
        }
    }

    /// <summary>
    /// Handle Cancel button click
    /// </summary>
    private void ButtonCancel_Click(object? sender, EventArgs e)
    {
        _logger.LogInformation("Cancel button clicked");

        var result = MessageBox.Show(
            "Are you sure you want to cancel the setup process?\n\nAny progress will be lost.",
            "Confirm Cancel",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _logger.LogInformation("Setup cancelled by user");
            this.Close();
        }
    }

    /// <summary>
    /// Handle form closing
    /// </summary>
    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        _logger.LogInformation("Main form closing");
    }

    /// <summary>
    /// Navigate to a specific step
    /// </summary>
    /// <param name="step">Step number to navigate to</param>
    private void NavigateToStep(int step)
    {
        _logger.LogInformation($"Navigating to step {step}");

        switch (step)
        {
            case 1:
                LoadWelcomeStep();
                break;
            case 2:
                LoadAuthenticationStep();
                break;
            case 3:
                LoadAppRegistrationStep();
                break;
            case 4:
                LoadDeploymentStep();
                break;
            case 5:
                LoadSqlStep();
                break;
            case 6:
                LoadExportStep();
                break;
            default:
                _logger.LogError($"Invalid step number: {step}");
                break;
        }

        UpdateNavigationButtons();
        UpdateProgress();
    }

    /// <summary>
    /// Validate the current step before proceeding
    /// </summary>
    /// <returns>True if validation passes</returns>
    private bool ValidateCurrentStep()
    {
        _logger.LogInformation($"Validating step {_currentStep}");

        // Step-specific validation will be implemented in future tasks
        switch (_currentStep)
        {
            case 1:
                // Welcome step - always valid
                return true;
            case 2:
                // Authentication step - to be implemented in Task 2.2
                return ValidateAuthenticationStep();
            case 3:
                // App registration step - to be implemented in Task 2.3
                return ValidateAppRegistrationStep();
            case 4:
                // Deployment step - to be implemented in Task 3.1
                return ValidateDeploymentStep();
            case 5:
                // SQL step - to be implemented in Task 3.2
                return ValidateSqlStep();
            case 6:
                // Export step - to be implemented in Task 3.3
                return ValidateExportStep();
            default:
                return false;
        }
    }

    /// <summary>
    /// Update navigation button states
    /// </summary>
    private void UpdateNavigationButtons()
    {
        buttonBack.Enabled = _currentStep > 1;
        
        if (_currentStep == _totalSteps)
        {
            buttonNext.Text = "Finish";
        }
        else
        {
            buttonNext.Text = "Next >";
        }
        
        _logger.LogInformation($"Navigation buttons updated for step {_currentStep}");
    }

    /// <summary>
    /// Update progress indicator
    /// </summary>
    private void UpdateProgress()
    {
        labelProgress.Text = $"Step {_currentStep} of {_totalSteps}";
        progressBar.Value = _currentStep;
        
        _logger.LogInformation($"Progress updated: {_currentStep}/{_totalSteps}");
    }

    /// <summary>
    /// Set status message
    /// </summary>
    /// <param name="message">Status message to display</param>
    private void SetStatus(string message)
    {
        labelStatus.Text = message;
        _logger.LogInformation($"Status updated: {message}");
    }

    /// <summary>
    /// Complete the setup process
    /// </summary>
    private void CompleteSetup()
    {
        _logger.LogInformation("Setup process completed");
        
        MessageBox.Show(
            "Email retention setup has been completed successfully!\n\n" +
            "Configuration files have been generated and are ready for use with the Operations application.",
            "Setup Complete",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        this.Close();
    }

    #region Step Loading Methods (Placeholders for Future Tasks)

    /// <summary>
    /// Load authentication step (Task 2.2)
    /// </summary>
    private void LoadAuthenticationStep()
    {
        panelMain.Controls.Clear();
        var placeholderLabel = new Label
        {
            Text = "Microsoft Graph Authentication\n\n(To be implemented in Task 2.2)",
            Font = new Font("Segoe UI", 12F),
            ForeColor = Color.FromArgb(102, 102, 102),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        panelMain.Controls.Add(placeholderLabel);
        SetStatus("Authentication configuration");
    }

    /// <summary>
    /// Load app registration step (Task 2.3)
    /// </summary>
    private void LoadAppRegistrationStep()
    {
        panelMain.Controls.Clear();
        var placeholderLabel = new Label
        {
            Text = "Azure AD App Registration\n\n(To be implemented in Task 2.3)",
            Font = new Font("Segoe UI", 12F),
            ForeColor = Color.FromArgb(102, 102, 102),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        panelMain.Controls.Add(placeholderLabel);
        SetStatus("App registration configuration");
    }

    /// <summary>
    /// Load deployment step (Task 3.1)
    /// </summary>
    private void LoadDeploymentStep()
    {
        panelMain.Controls.Clear();
        var placeholderLabel = new Label
        {
            Text = "Deployment Type Selection\n\n(To be implemented in Task 3.1)",
            Font = new Font("Segoe UI", 12F),
            ForeColor = Color.FromArgb(102, 102, 102),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        panelMain.Controls.Add(placeholderLabel);
        SetStatus("Deployment configuration");
    }

    /// <summary>
    /// Load SQL step (Task 3.2)
    /// </summary>
    private void LoadSqlStep()
    {
        panelMain.Controls.Clear();
        var placeholderLabel = new Label
        {
            Text = "SQL Server Connection Testing\n\n(To be implemented in Task 3.2)",
            Font = new Font("Segoe UI", 12F),
            ForeColor = Color.FromArgb(102, 102, 102),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        panelMain.Controls.Add(placeholderLabel);
        SetStatus("SQL connection configuration");
    }

    /// <summary>
    /// Load export step (Task 3.3)
    /// </summary>
    private void LoadExportStep()
    {
        panelMain.Controls.Clear();
        var placeholderLabel = new Label
        {
            Text = "Configuration Export\n\n(To be implemented in Task 3.3)",
            Font = new Font("Segoe UI", 12F),
            ForeColor = Color.FromArgb(102, 102, 102),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        panelMain.Controls.Add(placeholderLabel);
        SetStatus("Configuration export");
    }

    #endregion

    #region Step Validation Methods (Placeholders for Future Tasks)

    /// <summary>
    /// Validate authentication step (Task 2.2)
    /// </summary>
    private bool ValidateAuthenticationStep()
    {
        // Placeholder - always return true for now
        return true;
    }

    /// <summary>
    /// Validate app registration step (Task 2.3)
    /// </summary>
    private bool ValidateAppRegistrationStep()
    {
        // Placeholder - always return true for now
        return true;
    }

    /// <summary>
    /// Validate deployment step (Task 3.1)
    /// </summary>
    private bool ValidateDeploymentStep()
    {
        // Placeholder - always return true for now
        return true;
    }

    /// <summary>
    /// Validate SQL step (Task 3.2)
    /// </summary>
    private bool ValidateSqlStep()
    {
        // Placeholder - always return true for now
        return true;
    }

    /// <summary>
    /// Validate export step (Task 3.3)
    /// </summary>
    private bool ValidateExportStep()
    {
        // Placeholder - always return true for now
        return true;
    }

    #endregion
}
