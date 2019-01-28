# Loot
Loot is an ASP.NET 4.5 Web Forms application to track investments across multiple brokerage accounts.

## Architecture

### Web Application

The web application allows users to manage their various accounts and security holdings.

### Windows Service

The Windows service is responsible for pulling the security quotes from Yahoo, U.S. Department of Treasury, and various other data sources.  It's additional roles are to send alerts and performance maintenance.  It utilizes the [Quartz.net](https://github.com/quartznet/quartznet) library.