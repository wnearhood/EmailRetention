# Texas Municipality Email Retention Solution

A .NET solution for Texas municipal email retention compliance, providing automated email metadata extraction and permanent deletion logging to meet State of Texas library requirements.

## Overview

This solution consists of two applications:
- **Setup Application**: WinForms application for initial configuration and M365 setup
- **Operations Application**: Console application for scheduled email metadata extraction

## Features

- **Automated App Registration**: Creates and configures Azure AD applications with required permissions
- **Flexible Deployment**: Supports both local SQL Server and Azure SQL Database
- **Compliance Logging**: Captures complete email metadata before retention policy deletion
- **Texas State Compliance**: Meets Texas State Library and Archives Commission requirements
- **Audit Integration**: Optional Microsoft Purview integration for enhanced compliance

## Requirements

- .NET 8.0 or later
- Visual Studio 2022 (for development)
- Microsoft 365 tenant with Global Administrator access
- SQL Server (local) or Azure SQL Database
- Microsoft Graph API permissions

## Quick Start

1. Clone this repository
2. Open `EmailRetention.sln` in Visual Studio
3. Build the solution
4. Run the Setup application to configure your environment
5. Schedule the Operations application for automated metadata extraction

## Project Structure

```
/EmailRetention
├── /Setup              - WinForms setup application
├── /Operations         - Console operations application
├── /Shared            - Shared library with common code
├── /Documentation     - Project documentation and plans
├── /Scripts           - Utility scripts
└── EmailRetention.sln - Visual Studio solution
```

## Architecture

The solution uses Microsoft Graph API for email metadata extraction and supports both local and cloud-based SQL storage for compliance logging. It implements certificate-based authentication for unattended operations and provides comprehensive error handling and monitoring.

## Compliance

This solution is designed to meet:
- Texas State Library and Archives Commission retention schedules
- Texas Public Information Act requirements
- Federal records management standards
- SOC 2 compliance requirements

## Support

For documentation and implementation guides, see the `/Documentation` folder.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

---

**Developed for Texas municipalities requiring bulletproof email retention compliance.**