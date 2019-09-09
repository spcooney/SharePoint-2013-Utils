# Variables
$StartTime = "06/14/2017 05:00:00 PM"  # mm/dd/yyyy hh:mm:ss
$EndTime = "06/14/2017 05:10:00 PM"
$TimerJobName = "Timer Job Name"
 
#To Get Yesterday's use:
#$StartDateTime = (Get-Date).AddDays(-1).ToString('MM-dd-yyyy') + " 00:00:00"
#$EndDateTime   = (Get-Date).AddDays(-1).ToString('MM-dd-yyyy') + " 23:59:59"
 
#Get the specific Timer job
$Timerjob = Get-SPTimerJob | where { $_.DisplayName -eq $TimerJobName }
 
#Get all timer job history from the web application
$Results = $Timerjob.HistoryEntries  |
      where { ($_.StartTime -ge  $StartTime) -and ($_.EndTime -le $EndTime) } |
          Select *
 
#Send results to Grid view   
$Results | Out-GridView