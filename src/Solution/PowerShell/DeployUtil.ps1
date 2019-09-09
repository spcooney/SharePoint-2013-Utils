##############################################################################################################
#
#   Increases PowerShell window size for easier reading and buffering more output for recording errors
#
##############################################################################################################
function IncreaseHostWindowSize
{
	$pshost = get-host
	$pswindow = $pshost.ui.rawui
	$newsize = $pswindow.buffersize
	$newsize.height = 3000
	$newsize.width = 150
	$pswindow.buffersize = $newsize
	$newsize = $pswindow.windowsize
	$newsize.height = 50
	$newsize.width = 150
	$pswindow.windowsize = $newsize
}
##############################################################################################################
#
#   Adds a SharePoint solution to Central Administration
#
##############################################################################################################
function AddSolution
{
	param(
		[Parameter(Mandatory=$true)]
		[string]$WspLocation
	)
	HasSPSnapin
	Add-SPSolution -LiteralPath $WspLocation
}
##############################################################################################################
#
#   Upgrades a SharePoint solution
#
##############################################################################################################
function UpgradeSolution
{
	param(
		[Parameter(Mandatory=$true)]
		[string]$WspName,
		[Parameter(Mandatory=$true)]
		[string]$WspPath,
		[Switch]$Gac
	)
	HasSPSnapin
	$solution = Get-SPSolution | where-object { $_.Name -eq $WspName }
	if ($solution -ne $null)
	{
		WaitForJobToFinish $WspName	
		Write-Host "Updating solution $WspName..."
		if ($Gac)
		{
			Update-SPSolution -Identity $WspName -LiteralPath $WspPath -GACDeployment
		}
		else
		{
			Update-SPSolution -Identity $WspName -LiteralPath $WspPath	
		}
		Write-Host "Done"	
		WaitForJobToFinish $WspName	
		$solution = Get-SPSolution | where-object { $_.Name -eq $WspName }
		$result = $solution.LastOperationResult
		$details = $solution.LastOperationDetails.Trim()
		$endTime = $solution.LastOperationEndTime
		if ($solution.Deployed -eq $false)
		{
			Write-Host -f Red "Solution could not be updated"
			Write-Host -f Red "Result: $result."
			Write-Host -f Red "Details: $details."
			Write-Host -f Red "End time: $endTime"
		}
		Write-Host -f Green "Solution was updated successfully"
		Write-Host -f Green "Result: $result"
		Write-Host -f Green "Details: $details"
		Write-Host -f Green "End time: $endTime"
	}
	else
	{
		Write-Warning "Solution $WspName does not exist in the farm"
	}
}
##############################################################################################################
#
#   Waits for the SharePoint job to finish
#
##############################################################################################################
function WaitForJobToFinish
{
	param(
		[Parameter(Mandatory=$true)]
		[string]$SolutionFileName
	)
	Write-Host "Executing deployment jobs..."
	$jobName = "*solution-deployment*$SolutionFileName*"
	$job = Get-SPTimerJob | ?{ $_.Name -like $jobName }
	if ($job -eq $null)
	{
		Write-Host "There are not pending deployment jobs"
	}
	else 
	{
		$jobName = $job.Name
		Write-Host "Waiting for job to finish..."
		Write-Host $jobName    
		$timer = 0
		while ((Get-SPTimerJob $jobName) -ne $null) 
		{
			Start-Sleep -Seconds 3
			$timer = $timer + 1
			if ($timer -eq 66)
			{
				if (AskYesNo("The job did not finish", "Continue waiting?"))
				{
					Write-Host "Waiting for job to finish..."
					Write-Host $jobName
					$timer = 0
				}
				else
				{
					Write-Host -f Red "The job did not finish. Cannot continue."
					exit
				}
			}
		}
		Write-Host -f Green "Finished waiting for job"
		Write-Host $jobFullName
	}
}
##############################################################################################################
#
#   Asks the administrator if they want to continue to wait
#
##############################################################################################################
function AskYesNo()
{
	param (
		[string]$Caption,
		[string]$Message
	)	
	$yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes",""
	$no = New-Object System.Management.Automation.Host.ChoiceDescription "&No",""
	$Choices = [System.Management.Automation.Host.ChoiceDescription[]]($yes,$no)
	$Caption = "The job did not finish"
	$Message = "Continue waiting?"
	$Result = $Host.UI.PromptForChoice($Caption,$Message,$Choices,0)
	($Result -eq 0)
}
##############################################################################################################
#
#   Installs a SharePoint solution within Central Administration
#
##############################################################################################################
function InstallSolution
{
	param (
		[Parameter(Mandatory=$true)]
		[string]$WspName,
		[string]$WebAppUrl,
		[string]$CompatibilityLevel = {15}
	)
	HasSPSnapin
	Write-Host "Checking if the $WspName is already added"
	$Solution = Get-SPSolution $WspName -ErrorAction SilentlyContinue
	if ((!$Solution) -and ($Solution.Deployed -eq $true))
	{
		Write-Host "$WspName is already deployed"
	}
	elseif (($Solution) -and ($Solution.Added -eq $true))
	{
		if ($Solution.ContainsWebApplicationResource)
		{
			Install-SPSolution -Identity $WspName -GacDeployment -CompatibilityLevel $CompatibilityLevel -WebApplication $WebAppUrl
		}
		else
		{
			Install-SPSolution -Identity $WspName -GacDeployment -CompatibilityLevel $CompatibilityLevel
		}
		WaitForJobToFinish $WspName
		Start-Sleep -Seconds 10
		while (Get-SPSolution -Identity $WspName | where {$_.Deployed -ne $true})
		{
			ResetTimerService
			Start-Sleep -Seconds 10
			if ($Solution.ContainsWebApplicationResource)
			{
				Install-SPSolution -Identity $WspName -GacDeployment -CompatibilityLevel $CompatibilityLevel -WebApplication $WebAppUrl
			}
			else
			{
				Install-SPSolution -Identity $WspName -GacDeployment -CompatibilityLevel $CompatibilityLevel
			}
			WaitForJobToFinish $WspName
		}
	}
	else
	{
		Write-Host "$WspName has not been added yet"
	}
}
##############################################################################################################
#
#   Un-installs a SharePoint solution to Central Administration
#
##############################################################################################################
function UninstallSolution
{
	param (
		[Parameter(Mandatory=$true)]
		[string]$WspName,
		[string]$WebAppUrl,
		[string]$CompatibilityLevel
	)
	HasSPSnapin
	Write-Host "Checking if the $WspName is deployed"
	$Solution = Get-SPSolution $WspName -ErrorAction SilentlyContinue
	if (!$Solution)
	{
		Write-Host "$WspName was not found" -foreground "red"
		return;
	}
	if (($Solution.Added -eq $true) -or ($Solution.Deployed -eq $true))
	{
		if ($Solution.ContainsWebApplicationResource -and $WebAppUrl)
		{
			$WebAppName = Get-SPWebApplication $WebAppUrl | select name
			if ($WebAppName)
			{
				Uninstall-SPSolution -Identity $WspName -Confirm:$false -WebApplication $WebAppUrl
			}
			else
			{
				Uninstall-SPSolution -Identity $WspName -Confirm:$false -AllWebApplications
			}
		}
		else
		{
			Uninstall-SPSolution -Identity $WspName -Confirm:$false -AllWebApplications
		}
		WaitForJobToFinish $WspName
		Write-Host "$WspName has been un-installed"
		Write-Host "Removing $WspName"
		Remove-SPSolution -Identity $WspName -Confirm:$false
		Write-Host "$WspName has been removed"
	}
	else
	{
		Write-Host "$WspName is not installed"
	}
}
##############################################################################################################
#
#   Used to enable a feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function EnableFeature
{
	param (
		[string]$WebApplicationUrl,
		[string]$SiteUrl,
		[Parameter(Mandatory=$true)]
		[string]$FeatureGuid,
		[bool]$Force
	)
	HasSPSnapin
	$Feature = Get-SPFeature -Identity $FeatureGuid -ErrorAction SilentlyContinue
	if (!$Feature)
	{
		Write-Host "The feature $FeatureGuid was not found"
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Farm)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Farm -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-Host "The feature $FeatureGuid is already enabled on the farm"
			return;
		}
		else
		{
			Write-Host "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -Farm $SiteUrl
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::WebApplication)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -WebApplication $WebApplicationUrl -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-Warning "The feature $FeatureGuid is already enabled in the web application $WebApplicationUrl"
			return;
		}
		else
		{
			Write-Host "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -WebApplication $SiteUrl
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Site)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-Warning "The feature $FeatureGuid is already enabled in this site $SiteUrl"
			return;
		}
		else
		{
			Write-Host "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Web)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-Warning "The feature $FeatureGuid is already enabled in the web $SiteUrl"
			return;
		}
		else
		{
			Write-Host "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl
		}
	}
}
##############################################################################################################
#
#   Used to disable a feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function DisableFeature
{
	param (
		[string]$WebApplicationUrl,
		[string]$SiteUrl,
		[Parameter(Mandatory=$true)]
		[string]$FeatureGuid,
		[bool]$Force
	)
	HasSPSnapin
	$Feature = Get-SPFeature -Identity $FeatureGuid -ErrorAction SilentlyContinue
	if (!$Feature)
	{
		Write-Host "The feature $FeatureGuid was not found"
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Farm)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Farm -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-Host "The feature $FeatureGuid is not activated on the farm"
			return;
		}
		else
		{
			Write-Host "Disabling feature $FeatureGuid."		
			if ($Force -eq $false)
			{
				Disable-SPFeature -Identity $FeatureGuid -confirm:$false
			}
			else
			{
				Disable-SPFeature -Identity $FeatureGuid -confirm:$false -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is disabled"
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::WebApplication)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -WebApplication $WebApplicationUrl -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-Warning "The feature $FeatureGuid is not activated in the Web application $WebApplicationUrl"
			return;
		}
		else
		{
			Write-Host "Disabling feature $FeatureGuid."		
			if ($Force -eq $false)
			{
				Disable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -confirm:$false
			}
			else
			{
				Disable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -confirm:$false -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is disabled"
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Site)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-Warning "The feature $FeatureGuid is not activated in this site $SiteUrl"
			return;
		}
		else
		{
			Write-Host "Disabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
			}
			else
			{
				Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false -Force
			}		
			Write-Host -f Green "Feature $FeatureGuid is disabled"
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Web)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-Warning "The feature $FeatureGuid is not activated in the web $SiteUrl"
			return;
		}
		else
		{
			Write-Host "Disabling feature $FeatureGuid."		
			if ($Force -eq $false)
			{
				Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
			}
			else
			{
				Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is disabled"
		}
	}
}
##############################################################################################################
#
#   Used to disable a site feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function DisableSiteFeature
{
	param (
		[Parameter(Mandatory=$true)]
		[string]$SiteUrl,
		[Parameter(Mandatory=$true)]
		[string]$FeatureGuid,
		[bool]$Force
	)
	HasSPSnapin
	$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl -ErrorAction SilentlyContinue
	if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
	{
		Write-Warning "The feature $FeatureGuid is not activated in this site $SiteUrl"
		return;
	}
	else
	{
		Write-Host "Disabling feature $FeatureGuid."
		if ($Force -eq $false)
		{
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
		}
		else
		{
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false -Force
		}	
		Write-Host -f Green "Feature $FeatureGuid is disabled"
	}
}
##############################################################################################################
#
#   Used to disable a web feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function DisableWebFeature
{
	param (
		[Parameter(Mandatory=$true)]
		[string]$SiteUrl,
		[Parameter(Mandatory=$true)]
		[string]$FeatureGuid,
		[bool]$Force
	)
	HasSPSnapin
	$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl -ErrorAction SilentlyContinue
	if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
	{
		Write-Warning "The feature $FeatureGuid is not activated in this site $SiteUrl"
		return;
	}
	else
	{
		Write-Host "Disabling feature $FeatureGuid."		
		if ($Force -eq $false)
		{
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
		}
		else
		{
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false -Force
		}
		Write-Host -f Green "Feature $FeatureGuid is disabled"
	}
}
##############################################################################################################
#
#   Resets the SharePoint timer job service within the entire farm
#
##############################################################################################################
function ResetTimerService
{
	HasSPSnapin
	$Farm = Get-SPFarm
	$Farm.TimerService.Instances | foreach {$_.Stop();$_.Start();}
	[array]$Servers = Get-SPServer | ? {$_.Role -eq "Application"}
	foreach ($Server in $Servers)
	{
		Write-Host "Restarting Timer Service on $Server"
		$Service = Get-WmiObject -Computer $Server.name Win32_Service -Filter "Name='SPTimerV4'" 
		if ($Service -ne $null)
		{
			$Service.InvokeMethod('StopService', $null)
			$Service.InvokeMethod('StartService', $null)
			Start-Sleep -Seconds 20
			$TimerStatus = Get-WmiObject -Computer $Server.name Win32_Service -Filter "Name='SPTimerV4'"
			while ($TimerStatus.State -eq "Stopped" -or $TimerStatus.State -eq "Stop Pending")
			{               
				$service.InvokeMethod('StartService', $null)
				Start-Sleep -Seconds 20
				$TimerStatus = Get-WmiObject -Computer $Server.name Win32_Service -Filter "Name='SPTimerV4'"
				Write-Host -ForegroundColor Green "Current timer job state:" $TimerStatus.State on $Server
			}
			Write-Host -ForegroundColor Green "Timer Job successfully restarted on $Server"
		}
		else
		{ 
			Write-Host -ForegroundColor Red "Could not find SharePoint Timer Service on $Server"
		}
	}
}
##############################################################################################################
#
#   Tries to find an IIS application pool with the name provided
#
##############################################################################################################
function GetAppPoolProcID
{
	param (
		[Parameter(Mandatory=$true)]
		[string]$AppPoolName
	)
	HasWebAdminModule
	dir IIS:\AppPools | Get-ChildItem | Get-ChildItem | Where-Object {$_.appPoolName -eq $AppPoolName } | Format-Table -AutoSize  processId, appPoolName
}
##############################################################################################################
#
#   Checks if PowerShell has the Microsoft.SharePoint.Powershell snapin loaded, if not, it loads it
#
##############################################################################################################
function HasSPSnapin
{
	if (!(Get-PSSnapin | Where-Object { $_.Name -eq "Microsoft.SharePoint.Powershell" }))
	{
		Write-Host "Adding the SharePoint Powershell Snapin"
		Add-PSSnapin "Microsoft.SharePoint.Powershell"
	}
}
##############################################################################################################
#
#   Checks if PowerShell has the IIS Web Administration module loaded, if not, it loads it
#
##############################################################################################################
function HasWebAdminModule
{
	if (!(Get-Module | Where-Object { $_.Name -eq "webadministration" }))
	{
		Write-Host "Adding the IIS web administration module"
		import-module webadministration
	}
}