##############################################################################################################
#
#   Adds a SharePoint solution to Central Administration
#
##############################################################################################################
function AddSolution([string]$WspLocation)
{
	HasSPSnapin
	Add-SPSolution -LiteralPath $WspLocation
}
##############################################################################################################
#
#   Installs a SharePoint solution within Central Administration
#
##############################################################################################################
function InstallSolution([string]$WspName, [string]$WebAppUrl, [string]$CompatibilityLevel)
{
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
		$SolutionStatus = Get-SPSolution $WspName
		if ($SolutionStatus.Deployed -eq $false)
		{
			$counter = 1
			$maximum = 25
			$sleeptime = 10
			while ($SolutionStatus.JobExists -and ($counter -lt $maximum))
			{
				Write-Host "Installing $WspName, please wait..."
				sleep $sleeptime
				$counter++
			}
			Write-Host -f Green "$WspName at $WebAppUrl has been installed"
		}
	}
	else
	{
		Write-Host "$WspName has not been added yet"
	}
}
##############################################################################################################
#
#   Uninstalls a SharePoint solution to Central Administration
#
##############################################################################################################
function UninstallSolution([string]$WspName, [string]$CompatibilityLevel)
{
	HasSPSnapin
	Write-Host "Checking if the $WspName is deployed"
	$Solution = Get-SPSolution $WspName -ErrorAction SilentlyContinue
	if (($Solution.Added -eq $true) -or ($Solution.Deployed -eq $true))
	{
		Uninstall-SPSolution -Identity $WspName -Confirm:$false -AllWebApplications
		$counter = 1
		$maximum = 50
		$sleeptime = 10
		$SolutionStatus = Get-SPSolution $WspName
		while ($SolutionStatus.JobExists -and ($counter -lt $maximum))
		{
			Write-Host "Un-installing $WspName, please wait..."
			sleep $sleeptime
			$counter++
		}
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
			Write-Host "The feature $FeatureGuid is already enabled on the farm"
			return;
		}
		else
		{
			Write-Host "Enabling feature $FeatureGuid."
			if ($Force -eq $true)
			{
				Enable-SPFeature -Identity $FeatureGuid
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
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
			if ($Force -eq $true)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
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
			if ($Force -eq $true)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is disabled"
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
			if ($Force -eq $true)
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl
			}
			else
			{
				Enable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -Force
			}
			Write-Host -f Green "Feature $FeatureGuid is enabled"
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
			Disable-SPFeature -Identity $FeatureGuid -confirm:$false
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
			Disable-SPFeature -Identity $FeatureGuid -Url $WebApplicationUrl -confirm:$false
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
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
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
			Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
			Write-Host -f Green "Feature $FeatureGuid is disabled"
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
		Write-Warning "The feature $FeatureGuid is not activated in this site $SiteUrl"
		return;
	}
	else
	{
		Write-Host "Disabling feature $FeatureGuid."
		Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
		Write-Host -f Green "Feature $FeatureGuid is disabled"
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
		Write-Warning "The feature $FeatureGuid is not activated in this site $SiteUrl"
		return;
	}
	else
	{
		Write-Host "Disabling feature $FeatureGuid."
		Disable-SPFeature -Identity $FeatureGuid -Url $SiteUrl -confirm:$false
		Write-Host -f Green "Feature $FeatureGuid is disabled"
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
		Write-Host "Adding the SharePoint Powershell Snapin"
		Add-PSSnapin "Microsoft.SharePoint.Powershell"
	}
}