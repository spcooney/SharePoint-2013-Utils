# Reads/queries ULS trace logs.
Get-SPLogEvent | Where-Object {$_.Correlation -eq "c276860f-66a4-4b7e-bb14-b6f4ebf4091d" }

# Reads the ULS log entries between the start and end time
Get-SPLogEvent -StartTime "12/10/2015 15:00" -EndTime "12/10/2015 16:00" | Out-GridView

# Get all the errors above the minimum level
Get-SPLogEvent -MinimumLevel "Warning"

# Ends the current log file and starts a new one.
New-SPLogFile

# Returns IDiagnosticsLevel2 objects or displays a list of diagnostics levels.
Get-SPLogLevel

# Allows the user to set the trace and event level for a set of categories.
Set-SPLogLevel

# Resets the trace and event levels back to their default values.
Clear-SPLogLevel

# Combines trace log files from all farm servers into a single file.
Merge-SPLogFile