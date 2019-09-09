# Get all the timer jobs in the farm than ran within the last 30 minutes
Get-SPTimerJob | select Name, DisplayName, LastRunTime | Where-Object {$_.LastRunTime -gt (get-date).AddMinutes(-30)} | Sort-Object LastRunTime

# Get the deployment timer job
Get-SPTimerJob | ?{ $_.Name -like "*solution-deployment*WSPName*" } Select-Object *

# Find timer job by the display name
Get-SPTimerJob | select-object * | Where-Object { $_.DisplayName -like "Timer Job Name" }