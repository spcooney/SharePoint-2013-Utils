# Import the IIS module
import-module webadministration

# List all of the worker processes
dir IIS:\AppPools | Get-ChildItem | Get-ChildItem | Format-Table -AutoSize  processId, appPoolName

# Find a specific worker process by the application pool name
dir IIS:\AppPools | Get-ChildItem | Get-ChildItem | Where-Object {$_.appPoolName -eq "Application Pool Name" } | Format-Table -AutoSize  processId, appPoolName