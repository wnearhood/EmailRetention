using EmailRetention.Shared.Services;
using EmailRetention.Shared.Services.Interfaces;

namespace EmailRetention.Setup;

/// <summary>
/// Main entry point for the Email Retention Setup Application
/// </summary>
internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Configure application for high DPI awareness
        ApplicationConfiguration.Initialize();
        
        // Set up global exception handling
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        try
        {
            // Initialize logging service
            var logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "EmailRetentionSetup",
                "Logs");
            
            var logger = new FileLoggerService(logPath);
            logger.LogInformation("Email Retention Setup Application Starting");

            // Start the main form
            Application.Run(new MainForm(logger));
            
            logger.LogInformation("Email Retention Setup Application Closing");
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"A critical error occurred during application startup:\n\n{ex.Message}", 
                "Startup Error", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handle UI thread exceptions
    /// </summary>
    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        HandleException(e.Exception, "UI Thread Exception");
    }

    /// <summary>
    /// Handle non-UI thread exceptions
    /// </summary>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            HandleException(ex, "Unhandled Exception");
        }
    }

    /// <summary>
    /// Centralized exception handling
    /// </summary>
    private static void HandleException(Exception ex, string context)
    {
        try
        {
            // Try to log the exception
            var logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "EmailRetentionSetup",
                "Logs");
            
            var logger = new FileLoggerService(logPath);
            logger.LogError($"{context}: {ex}");
        }
        catch
        {
            // If logging fails, continue with user notification
        }

        // Show user-friendly error message
        var result = MessageBox.Show(
            $"An unexpected error occurred:\n\n{ex.Message}\n\nWould you like to continue running the application?",
            $"Error - {context}",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Error);

        if (result == DialogResult.No)
        {
            Application.Exit();
        }
    }
}
