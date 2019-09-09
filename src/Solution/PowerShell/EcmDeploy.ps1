##############################################################################################################
#
#   Installs a SharePoint solution within Central Administration
#
##############################################################################################################
function InstallSolution
{
	param($WspPath, $WspName, $WebAppUrl, $CompatibilityLevel)

	HasSPSnapin
	$solution = Get-SPSolution | where-object { $_.Name -eq $WspName }
	if ($solution -eq $null)
	{
		Write-InstallStep "Adding solution $WspName..."
		$solution = Add-SPSolution -LiteralPath $WspPath -ErrorAction SilentlyContinue -ErrorVariable err
		CheckError(-1)
		Write-InstallStepDone
		if ($WebAppUrl -ne $null)
		{
			Write-InstallStep "Installing solution $WspName"
			Write-InstallMessage "to $WebAppUrl..."
		}
		else {
			Write-InstallStep "Installing solution $WspName globally..."
		}	
		if ($WebAppUrl -eq $null)
		{
			Install-SPSolution -Identity $WspName -GacDeployment -CompatibilityLevel $CompatibilityLevel -Force -ErrorAction SilentlyContinue -ErrorVariable err
		}
		else
		{
			Install-SPSolution -Identity $WspName -GacDeployment -CompatibilityLevel $CompatibilityLevel -WebApplication $WebAppUrl -Force -ErrorAction SilentlyContinue -ErrorVariable err
		}
		CheckError(-2)
		Write-InstallStepDone
		WaitForJobToFinish $WspName
		$solution = Get-SPSolution | where-object { $_.Name -eq $WspName }
		$result = $solution.LastOperationResult
		$details = $solution.LastOperationDetails.Trim()
		$endTime = $solution.LastOperationEndTime
		if ($solution.Deployed -eq $false)
		{
			Write-InstallError "Solution could not be installed"
			Write-InstallError "Result: $result."
			Write-InstallError "Details: $details."
			Write-InstallError "End time: $endTime"
			$lastExitCode = -3
			exit $lastExitCode
		}
		Write-InstallSuccessMessage "Solution was installed successfully"
		Write-InstallSuccessMessage "Result: $result"
		Write-InstallSuccessMessage "Details: $details"
		Write-InstallSuccessMessage "End time: $endTime"
	}
	else
	{
		Write-InstallWarning "Solution $name is already installed"
	}
}
##############################################################################################################
#
#   Un-installs a SharePoint solution to Central Administration
#
##############################################################################################################
function UninstallSolution
{
	param($WspName, $WebAppUrl)
	
	HasSPSnapin
	$solution = Get-SPSolution | where-object { $_.Name -eq $wspName }
	if ($solution -ne $null)
	{
		if($solution.Deployed -eq $true)
		{	
			if ($WebAppUrl -eq $null)
			{
				Write-InstallStep "Un-installing solution $wspName globally..."
				Uninstall-SPSolution -Identity $wspName -Confirm:$false -ErrorAction SilentlyContinue -ErrorVariable err
			} 
			else
			{
				Write-InstallStep "Un-installing solution $wspName"
				Write-InstallMessage "from $WebAppUrl..."
				Uninstall-SPSolution -Identity $wspName -Confirm:$false -WebApplication $WebAppUrl -ErrorAction SilentlyContinue -ErrorVariable err
			}
			CheckError(-4)
			Write-InstallStepDone
			WaitForJobToFinish $wspName
		}
		else
		{
			Write-InstallWarning "Solution $wspName is not installed"
		}
		Write-InstallStep "Deleting solution $wspName..."
		Remove-SPSolution -Identity $wspName -Confirm:$false -force -ErrorAction SilentlyContinue -ErrorVariable err
		CheckError(-5)
		Write-InstallStepDone
	}
	else
	{
		Write-InstallWarning "Solution $name does not exist in the farm"
	}
}
##############################################################################################################
#
#   Upgrades a SharePoint solution
#
##############################################################################################################
function UpgradeSolution
{
	param($name, [Switch]$gac)
	
	HasSPSnapin
	$solution = Get-SPSolution | where-object { $_.Name -eq $name }
	if ($solution -ne $null)
	{
		WaitForJobToFinish $name	
		Write-InstallStep "Updating solution $name..."
		$wspPath = (Get-Location).Path + "\" + $name
		Update-SPSolution -Identity $name -LiteralPath $wspPath -Gac
		Write-InstallStepDone	
		WaitForJobToFinish $name	
		$solution = Get-SPSolution | where-object { $_.Name -eq $name }
		$result = $solution.LastOperationResult
		$details = $solution.LastOperationDetails.Trim()
		$endTime = $solution.LastOperationEndTime
		if ($solution.Deployed -eq $false)
		{
			Write-InstallError "Solution could not be updated"
			Write-InstallError "Result: $result."
			Write-InstallError "Details: $details."
			Write-InstallError "End time: $endTime"
			$lastExitCode = -3
			exit $lastExitCode
		}
		Write-InstallSuccessMessage "Solution was updated successfully"
		Write-InstallSuccessMessage "Result: $result"
		Write-InstallSuccessMessage "Details: $details"
		Write-InstallSuccessMessage "End time: $endTime"
	}
	else
	{
		Write-InstallWarning "Solution $name does not exist in the farm"
	}
}
##############################################################################################################
#
#   Waits for the SharePoint job to finish
#
##############################################################################################################
function WaitForJobToFinish([string]$solutionFileName)
{ 
	Write-InstallStep "Executing deployment jobs..."
	$jobName = "*solution-deployment*$solutionFileName*"
	$job = Get-SPTimerJob | ?{ $_.Name -like $jobName }
	if ($job -eq $null)
	{
		Write-InstallMessage "There are not pending deployment jobs"
	}
	else 
	{
		$jobName = $job.Name
		Write-InstallMessage "Waiting for job to finish..."
		Write-InstallMessage $jobName    
		$timer = 0
		while ((Get-SPTimerJob $jobName) -ne $null) 
		{
			Start-Sleep -Seconds 3
			$timer = $timer + 1
			if ($timer -eq 66)
			{
				if (AskYesNo("The job did not finish", "Continue waiting?"))
				{
					Write-InstallMessage "Waiting for job to finish..."
					Write-InstallMessage $jobName
					$timer = 0
				}
				else
				{
					Write-InstallError "The job did not finish. Cannot continue."
					$lastExitCode = -8
					exit $lastExitCode
				}
			}
		}
		Write-InstallMessage "Finished waiting for job"
		Write-InstallMessage $jobFullName
		Write-InstallStepDone
	}
}
##############################################################################################################
#
#   Asks the administrator if they want to continue to wait
#
##############################################################################################################
function AskYesNo()
{
	param ($caption, $message)
	
	$yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes",""
	$no = New-Object System.Management.Automation.Host.ChoiceDescription "&No",""
	$choices = [System.Management.Automation.Host.ChoiceDescription[]]($yes,$no)
	$caption = "The job did not finish"
	$message = "Continue waiting?"
	$result = $Host.UI.PromptForChoice($caption,$message,$choices,0)
	($result -eq 0)
}
##############################################################################################################
#
#   Used to enable a feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function EnableFeature([string]$WebApplicationUrl, [string]$SiteUrl, [string]$FeatureGuid, [bool]$Force)
{
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
			Write-InstallWarning "The feature $FeatureGuid is already enabled on the farm"
			return;
		}
		else
		{
			Write-InstallMessage "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Force
			}
			Write-InstallSuccessMessage "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -Farm $SiteUrl
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::WebApplication)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -WebApplication $WebApplicationUrl -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-InstallWarning "The feature $FeatureGuid is already enabled in the web application $WebApplicationUrl"
			return;
		}
		else
		{
			Write-InstallMessage "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -Force
			}
			Write-InstallSuccessMessage "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -WebApplication $SiteUrl
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Site)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-InstallWarning "The feature $FeatureGuid is already enabled in this site $SiteUrl"
			return;
		}
		else
		{
			Write-InstallMessage "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -Force
			}
			Write-InstallSuccessMessage "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Web)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl -ErrorAction SilentlyContinue
		if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Online"))
		{
			Write-InstallWarning "The feature $FeatureGuid is already enabled in the web $SiteUrl"
			return;
		}
		else
		{
			Write-InstallMessage "Enabling feature $FeatureGuid."
			if ($Force -eq $false)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -Force
			}
			Write-InstallSuccessMessage "Feature $FeatureGuid is enabled"
			Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl
		}
	}
}
##############################################################################################################
#
#   Used to disable a feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function DisableFeature([string]$WebApplicationUrl, [string]$SiteUrl, [string]$FeatureGuid)
{
	HasSPSnapin
	$Feature = Get-SPFeature -Identity $FeatureGuid -ErrorAction SilentlyContinue
	if (!$Feature)
	{
		Write-InstallWarning "The feature $FeatureGuid was not found"
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Farm)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Farm -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-InstallWarning "The feature $FeatureGuid is not activated on the farm"
			return;
		}
		else
		{
			Write-InstallMessage "Disabling feature $FeatureGuid."
			Disable-SPFeature -Identity $FeatureGuid -confirm:$false
			Get-SPFeature -Identity $FeatureGuid
			Write-InstallSuccessMessage "Feature $FeatureGuid is disabled"
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::WebApplication)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -WebApplication $WebApplicationUrl -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-InstallWarning "The feature $FeatureGuid is not activated in the Web application $WebApplicationUrl"
			return;
		}
		else
		{
			Write-InstallMessage "Disabling feature $FeatureGuid."
			Disable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -confirm:$false
			Get-SPFeature -Identity $FeatureGuid
			Write-InstallSuccessMessage "Feature $FeatureGuid is disabled"
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Site)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-InstallWarning "The feature $FeatureGuid is not activated in this site $SiteUrl"
			return;
		}
		else
		{
			Write-InstallMessage "Disabling feature $FeatureGuid."
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
			Get-SPFeature -Identity $FeatureGuid
			Write-InstallSuccessMessage "Feature $FeatureGuid is disabled"
		}
	}
	elseif ($Feature.Scope -eq [Microsoft.SharePoint.SPFeatureScope]::Web)
	{
		$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl -ErrorAction SilentlyContinue
		if ((!$IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
		{
			Write-InstallWarning "The feature $FeatureGuid is not activated in the web $SiteUrl"
			return;
		}
		else
		{
			Write-InstallMessage "Disabling feature $FeatureGuid."
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
			Get-SPFeature -Identity $FeatureGuid
			Write-InstallSuccessMessage "Feature $FeatureGuid is disabled"
		}
	}
}
##############################################################################################################
#
#   Used to disable a site feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function DisableSiteFeature([string]$SiteUrl, [string]$FeatureGuid)
{
	HasSPSnapin
	$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Site $SiteUrl -ErrorAction SilentlyContinue
	if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
	{
		Write-InstallWarning "The feature $FeatureGuid is not activated in this site $SiteUrl"
		return;
	}
	else
	{
		Write-InstallMessage "Disabling feature $FeatureGuid."
		Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
		Get-SPFeature -Identity $FeatureGuid
		Write-InstallSuccessMessage "Feature $FeatureGuid is disabled"
	}
}
##############################################################################################################
#
#   Used to disable a web feature.  Takes an absolute site url and a feature unique identifier (GUID)
#
##############################################################################################################
function DisableWebFeature([string]$SiteUrl, [string]$FeatureGuid)
{
	HasSPSnapin
	$IsActiveFeature = Get-SPFeature -Identity $FeatureGuid -Web $SiteUrl -ErrorAction SilentlyContinue
	if (($IsActiveFeature) -and ($IsActiveFeature.Status -eq "Offline"))
	{
		Write-InstallWarning "The feature $FeatureGuid is not activated in this site $SiteUrl"
		return;
	}
	else
	{
		Write-InstallMessage "Disabling feature $FeatureGuid."
		Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
		Get-SPFeature -Identity $FeatureGuid
		Write-InstallSuccessMessage "Feature $FeatureGuid is disabled"
	}
}
##############################################################################################################
#
#   Checks if PowerShell has the Microsoft.SharePoint.Powershell snapin loaded, if not, it loads it
#
##############################################################################################################
function HasSPSnapin()
{
	if (!(Get-PSSnapin | Where-Object { $_.Name -eq "Microsoft.SharePoint.Powershell" }))
	{
		Write-InstallMessage "Adding the SharePoint Powershell Snapin"
		Add-PSSnapin "Microsoft.SharePoint.Powershell"
	}
}
##############################################################################################################
#
#   Checks if an error is present, if found, it will stop the process
#
##############################################################################################################
function CheckError
{
	param($errorCode)

	if ($err)
	{
		Write-InstallError $err
		$lastExitCode = $errorCode
		exit $lastExitCode
	}
}
##############################################################################################################
#
#   Gets the lines from the window
#
##############################################################################################################
function Get-Lines($message) 
{
	if ($message -eq "")
	{
		return $message
	}
	$lines = @()
	$windowWidth = (Get-Host).UI.RawUI.WindowSize.Width
	if ($windowWidth -eq $null)
	{
		$windowWidth = 80
	}
	$width = $windowWidth - 23
	for ($i=0; $i -lt $message.Length; $i+=$width)
	{
		$lines += $message.Substring($i, [System.Math]::Min($width, $message.Length - $i))
	}
	$lines
}
##############################################################################################################
#
#   The default message logging
#
##############################################################################################################
function Write-Host-Timestamp($prefix = "", $message, [string]$foreground = $null, [Switch]$noNewLine) 
{
	foreach ($text in Get-Lines($message))
	{
		$text = ("[" + (Get-Date -format 'dd/MM/yyyy').Substring(0, 6) + (Get-Date -format 'dd/MM/yyyy').Substring(8, 2) + " " + (Get-Date -format T) + "] ") + $prefix + $text
		if (-not $noNewLine)
		{
			$text += "`r"
		}
		$params = @{} 
		if ($noNewLine)
		{
			$params.Add("NoNewLine", $true)
		}
		if (($foreground -ne $null) -and ($foreground -ne ""))
		{
			$params.Add("ForegroundColor", $foreground)
		}
		Write-Host @params $text
	}
}
##############################################################################################################
#
#   Writes an install step to the window
#
##############################################################################################################
function Write-InstallStep([string]$message)
{
	Write-Host-Timestamp "" "> $message"
}
##############################################################################################################
#
#   Writes an install message to the window
#
##############################################################################################################
function Write-InstallMessage([string]$message)
{
	Write-Host-Timestamp "  " $message
}
##############################################################################################################
#
#   Writes an install warning to the window
#
##############################################################################################################

function Write-InstallWarning([string]$message)
{
	Write-Host-Timestamp "  " $message -Foreground Yellow
}
##############################################################################################################
#
#   Writes an install success to the window
#
##############################################################################################################

function Write-InstallSuccessMessage([string]$message)
{
	Write-Host-Timestamp "  " $message -Foreground Green
}
##############################################################################################################
#
#   Writes an install error to the window
#
##############################################################################################################
function Write-InstallError([string]$message)
{
	Write-Host-Timestamp "  " $message -Foreground Red
}
##############################################################################################################
#
#   Writes Done. to the window
#
##############################################################################################################
function Write-InstallStepDone()
{
	Write-InstallMessage "Done."
}