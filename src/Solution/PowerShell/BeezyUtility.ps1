function CheckError {
	param($errorCode)

	if ($err) {
		Write-InstallError $err
		$LastExitCode = $errorCode
		exit $LastExitCode
	}
}

function UpgradeSolution {
	param($name, [Switch]$Gac)

	$solution = Get-SPSolution | where-object {$_.Name -eq $name}
	if ($solution -ne $null) {
		WaitForJobToFinish $name
		
		Write-InstallStep "Updating solution $name..."
		$wspPath = (Get-Location).Path + "\" + $name
		Update-SPSolution -Identity $name -LiteralPath $wspPath -Gac
		Write-InstallStepDone
		
		WaitForJobToFinish $name
		
		$solution = Get-SPSolution | where-object {$_.Name -eq $name}
		$result = $solution.LastOperationResult
		$details = $solution.LastOperationDetails.Trim()
		$endTime = $solution.LastOperationEndTime
		if ($solution.Deployed -eq $false) {
			Write-InstallError "Solution could not be updated"
			Write-InstallError "Result: $result."
			Write-InstallError "Details: $details."
			Write-InstallError "End time: $endTime"
			$LastExitCode = -3
			exit $LastExitCode
		}

		Write-InstallSuccessMessage "Solution was updated successfully"
		Write-InstallSuccessMessage "Result: $result"
		Write-InstallSuccessMessage "Details: $details"
		Write-InstallSuccessMessage "End time: $endTime"
	}
	else {
		Write-InstallWarning "Solution $name does not exist in the farm"
	}
}

function InstallSolution {
	param($name, $url)

	$wspPath = (Get-Location).Path + "\" + $name

	$solution = Get-SPSolution | where-object {$_.Name -eq $name}
	if ($solution -eq $null) {
		Write-InstallStep "Adding solution $name..."
		$solution = Add-SPSolution -LiteralPath $wspPath -ErrorAction SilentlyContinue -ErrorVariable err
		CheckError(-1)
		Write-InstallStepDone
		
		if ($url -ne $null) {
			Write-InstallStep "Installing solution $name"
			Write-InstallMessage "to $url..."
		}
		else {
			Write-InstallStep "Installing solution $name globally..."
		}
		
		if ($url -eq $null) {
			Install-SPSolution -Identity $name -GacDeployment -Force -ErrorAction SilentlyContinue -ErrorVariable err
		}
		else {
			Install-SPSolution -Identity $name -GacDeployment -WebApplication $url -Force -ErrorAction SilentlyContinue -ErrorVariable err
		}

		CheckError(-2)
		Write-InstallStepDone

		WaitForJobToFinish $name

		$solution = Get-SPSolution | where-object {$_.Name -eq $name}
		$result = $solution.LastOperationResult
		$details = $solution.LastOperationDetails.Trim()
		$endTime = $solution.LastOperationEndTime
		if ($solution.Deployed -eq $false) {
			Write-InstallError "Solution could not be installed"
			Write-InstallError "Result: $result."
			Write-InstallError "Details: $details."
			Write-InstallError "End time: $endTime"
			$LastExitCode = -3
			exit $LastExitCode
		}

		Write-InstallSuccessMessage "Solution was installed successfully"
		Write-InstallSuccessMessage "Result: $result"
		Write-InstallSuccessMessage "Details: $details"
		Write-InstallSuccessMessage "End time: $endTime"
	}
	else {
		Write-InstallWarning "Solution $name is already installed"
	}
}

function UninstallSolution {
	param($name, $url)

	$solution = Get-SPSolution | where-object {$_.Name -eq $name}
	if ($solution -ne $null) {
		if($solution.Deployed -eq $true) {
		
			if ($url -eq $null) {
				Write-InstallStep "Uninstalling solution $name globally..."
				Uninstall-SPSolution -Identity $name -Confirm:$false -ErrorAction SilentlyContinue -ErrorVariable err
			} 
			else {
				Write-InstallStep "Uninstalling solution $name"
				Write-InstallMessage "from $url..."
				Uninstall-SPSolution -Identity $name -Confirm:$false -WebApplication $url -ErrorAction SilentlyContinue -ErrorVariable err
			}
			CheckError(-4)
			Write-InstallStepDone

			WaitForJobToFinish $name
		}
		else {
			Write-InstallWarning "Solution $name is not installed"
		}

		Write-InstallStep "Deleting solution $name..."
		Remove-SPSolution -Identity $name -Confirm:$false -force -ErrorAction SilentlyContinue -ErrorVariable err
		CheckError(-5)
		Write-InstallStepDone
	}
	else {
		Write-InstallWarning "Solution $name does not exist in the farm"
	}
}

function CreateSite {
	param($url, $template, $name, $description, $lcid, $siteOwner)

	if ((Get-SPSite $url -ea SilentlyContinue) -eq $null) {
		Write-InstallStep "Creating site collection $url"
		Write-InstallMessage "with template $template / owner $siteOwner / lcid $lcid ..."
		New-SPSite -Url $url -OwnerAlias $siteOwner -Confirm:$false -Description $description -Language $lcid -Name $name -Template $template -ErrorAction SilentlyContinue -ErrorVariable err > $null
		CheckError(-6)
		Write-InstallStepDone
	}
	else {
		Write-InstallWarning "Site collection $url already exists"
	}
}

function DeleteSite {
	param($url)

	if ((Get-SPSite $url -ea SilentlyContinue) -ne $null) {
		Write-InstallStep "Deleting site collection $url..."
		Remove-SPSite -Identity $url -Confirm:$false -ErrorAction SilentlyContinue -ErrorVariable err > $null
		CheckError(-7)
		Write-InstallStepDone
	}
	else {
		Write-InstallWarning "Site collection $url does not exist"
	}
}
 
function WaitForJobToFinish([string]$SolutionFileName)
{ 
	Write-InstallStep "Executing deployment jobs..."
    $JobName = "*solution-deployment*$SolutionFileName*"
    $job = Get-SPTimerJob | ?{ $_.Name -like $JobName }
    if ($job -eq $null) {
        Write-InstallMessage "There are not pending deployment jobs"
    }
    else {
        $JobFullName = $job.Name
        Write-InstallMessage "Waiting for job to finish..."
		Write-InstallMessage $JobFullName
        
		$timer = 0
        while ((Get-SPTimerJob $JobFullName) -ne $null) 
        {
            Start-Sleep -Seconds 3
			$timer = $timer + 1
			if ($timer -eq 66) {
				if (AskYesNo("The job did not finish" , "Continue waiting?")) {
					Write-InstallMessage "Waiting for job to finish..."
					Write-InstallMessage $JobFullName
					$timer = 0
				}
				else {
					Write-InstallError "The job did not finish. Cannot continue."
					$LastExitCode = -8
					exit $LastExitCode
				}
			}
        }
        Write-InstallMessage "Finished waiting for job"
		Write-InstallMessage $JobFullName
		Write-InstallStepDone
    }
}

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

function WaitForBeezyJobToFinish([string]$JobName)
{ 
	Write-InstallStep "Executing deployment jobs..."
	$job = Get-SPTimerJob | ?{ $_.Name -like $JobName }
    if ($job -eq $null) {
        Write-InstallMessage "There are not pending deployment jobs"
    }
    else {
		$JobFullName = $job.Name
        Write-InstallMessage "Waiting for job to finish..."
		Write-InstallMessage $JobFullName
        
		$timer = 0
        while ((Get-SPTimerJob $JobFullName) -ne $null) 
        {
            Start-Sleep -Seconds 2
			$timer = $timer + 1
			if ($timer -eq 60) {
				$LastExitCode = -8
				exit $LastExitCode
			}
        }
        Write-InstallMessage "Finished waiting for job"
		Write-InstallMessage $JobFullName
		Write-InstallStepDone
    }
}

function EnableFeatureSafe([string]$featureIdentity, [string]$url)
{
	$featureName = $featureIdentity.Replace("Spenta.Beezy_", "")
	Write-InstallStep "Enabling feature $featureName..."

	if ((Get-SPFeature $featureIdentity -ErrorAction SilentlyContinue) -eq $null) {
		Write-InstallWarning "Feature not found"
		return
	}

	Enable-SPFeature -identity $featureIdentity -url $url -Force
	Write-InstallStepDone
}

function EnsureSharePointWebSitesOnline() 
{
	$contentAppPools = [Microsoft.SharePoint.Administration.SPWebService]::ContentService.ApplicationPools | Select-Object -Property Name
	if ($contentAppPools -ne $null) {
		$contentAppPools | Foreach-Object { EnsureWebSitesOnline($_.Name) }
	}
	else {
		Write-InstallMessage "ContentService.ApplicationPools is null"
	}
	$adminAppPools = [Microsoft.SharePoint.Administration.SPWebService]::AdministrationService.ApplicationPools | Select-Object -Property Name
	if ($adminAppPools -ne $null) {
		$adminAppPools | Foreach-Object { EnsureWebSitesOnline($_.Name) }
	}
	else {
		Write-InstallMessage "AdministrationService.ApplicationPools is null"
	}
}

function EnsureWebSitesOnline([string]$appPoolName)
{
	Import-Module WebAdministration
	
	$IISError = $null
	<#$stoppedSites = Get-Website -ErrorAction SilentlyContinue -ErrorVariable $IISError | where-object { $_.State -ne "Started" }
	if ($stoppedSites -ne $null) {
		Restart-IIS
		foreach ($stoppedSite in $stoppedSites) {
			Restart-Webitem ("IIS:\Sites\" + $stoppedSite.name) -ErrorAction SilentlyContinue -ErrorVariable $IISError
		}
	}#>
	
	if (((Get-WebAppPoolState $appPoolName -ErrorAction SilentlyContinue -ErrorVariable $IISError).Value) -ne "Started") {
	
		Start-WebAppPool $appPoolName -ErrorAction SilentlyContinue -ErrorVariable $IISError
		while (((Get-WebAppPoolState $appPoolName -ErrorAction SilentlyContinue -ErrorVariable $IISError).Value) -ne "Started") {
			Write-InstallMessage "Waiting for Web application pool to start..."
			Start-Sleep -s 2
		}
		
		$stoppedSites = Get-Website -ErrorAction SilentlyContinue -ErrorVariable $IISError | where-object { ($_.State -ne "Started") -and ($_.applicationPool -eq $appPoolName) }
		
		if ($stoppedSites -ne $null) {
			foreach ($stoppedSite in $stoppedSites) {
				if ($stoppedSite -ne $null) {
					Restart-Webitem ("IIS:\Sites\" + $stoppedSite.name) -ErrorAction SilentlyContinue -ErrorVariable $IISError
				}
			}
		}
	}
	
	if ($IISError -ne $null) {
		Write-InstallMessage "Error occurred: $IISError"
		Restart-IIS
	}
}

function RecycleSiteAppPool([string]$SiteCollectionUrl)
{
	$site = Get-SPSite $SiteCollectionUrl -ea SilentlyContinue
	if ($site -ne $null) {
		$appPool = $site.WebApplication.ApplicationPool.DisplayName
		RecycleAppPool $appPool
	}
}

function RecycleAppPool([string]$appPoolName)
{
	Write-InstallStep "Restarting Web application pool $appPoolName..." 
	if ((Get-Module WebAdministration) -eq $null) { Import-Module WebAdministration }
	try {
		if (((Get-WebAppPoolState $appPoolName).Value) -eq "Started") {
			Restart-WebAppPool $appPoolName
			while (((Get-WebAppPoolState $appPoolName).Value) -ne "Started") {
				Write-InstallMessage "Waiting for Web application pool to start..."
				Start-Sleep -s 2
			}
		}
		else {
			Start-WebAppPool $appPoolName
			while (((Get-WebAppPoolState $appPoolName).Value) -ne "Started") {
				Write-InstallMessage "Waiting for Web application pool to start..."
				Start-Sleep -s 2
			}
		}
	}
	catch [System.Exception] {
		try {
			if (((Get-WebAppPoolState $appPoolName).Value) -ne "Started") {
				Start-WebAppPool $appPoolName
				while (((Get-WebAppPoolState $appPoolName).Value) -ne "Started") {
					Write-InstallMessage "Waiting for Web application pool to start..."
					Start-Sleep -s 2
				}
			}
		}
		catch [System.Exception] { }
	}
	Write-InstallStepDone
}

function Get-WebPage([string]$url)
{
	Add-Type @'
		public class MyWebClient : System.Net.WebClient
		{
			public int Timeout;
			
			protected override System.Net.WebRequest GetWebRequest(System.Uri uri)
			{
				System.Net.WebRequest w = base.GetWebRequest(uri);
				w.Timeout = this.Timeout;
				return w;
			}
		}
'@ ;
	
	Write-InstallStep "Warming up URL $url..."
	$wc = new-object MyWebClient;
	$wc.credentials = [System.Net.CredentialCache]::DefaultCredentials;
	$wc.Timeout = 5 * 60 * 1000;
	$pageContents = $wc.DownloadString($url);
	$wc.Dispose();
	Write-InstallStepDone
	return $pageContents;
}

function Get-WebStatusCode([string]$url)
{
	$wc = new-object net.webclient;
	$wc.credentials = [System.Net.CredentialCache]::DefaultCredentials;
	
	try {
		$pageContents = $wc.DownloadString($url);
	}
	catch [System.Exception] {
		$Exception = $Error[0].Exception
		if ($Exception.InnerException -ne $null -and $Exception.InnerException.Status -eq [System.Net.WebExceptionStatus]::ProtocolError)
		{
		   $response = [System.Net.HttpWebResponse]$Exception.InnerException.Response;             
		   $wc.Dispose();
		   return [int]$response.StatusCode;
		}	
		
		return "Unidentified"
	}
	
	$wc.Dispose();
	
	return 200;
}

function Get-WebObject([string]$url)
{
	$wc = new-object net.webclient;
	$wc.credentials = [System.Net.CredentialCache]::DefaultCredentials;
	
	try {
		$json = $wc.DownloadString($url);
		$assembly = [System.Reflection.Assembly]::LoadWithPartialName("System.Web.Extensions")
		$ser = New-Object System.Web.Script.Serialization.JavaScriptSerializer
		$obj = $ser.DeserializeObject($json)
	}
	catch [System.Exception] {
		$Exception = $Error[0].Exception
		if ($Exception.InnerException -ne $null -and $Exception.InnerException.Status -eq [System.Net.WebExceptionStatus]::ProtocolError)
		{
		   $response = [System.Net.HttpWebResponse]$Exception.InnerException.Response;             
		   $wc.Dispose();
		   return [int]$response.StatusCode;
		}	
		
		return "Unidentified"
	}
	
	$wc.Dispose();
	
	$obj;
}

function AssignRoleToPrincipalInWeb([Microsoft.SharePoint.SPWeb]$web, [Microsoft.SharePoint.SPPrincipal]$principal, 
	[Microsoft.SharePoint.SPRoleType]$roleType, [bool]$removeExistingRoles)
{
	if ($principal -ne $null)
	{
		if (-not $web.HasUniqueRoleAssignments) {
			$web.BreakRoleInheritance($true)
		}

		if ($removeExistingRoles)
		{
			$assignmentToRemove = $null
			try {
				$assignmentToRemove = $web.RoleAssignments.GetAssignmentByPrincipal($principal);
			}
			catch {}
			if ($assignmentToRemove -ne $null) {
				$assignmentToRemove.RoleDefinitionBindings.RemoveAll();
				$assignmentToRemove.Update();
			}
		}

		$assignment = new-object Microsoft.SharePoint.SPRoleAssignment($principal);
		$role = $web.RoleDefinitions.GetByType($roleType);
		$assignment.RoleDefinitionBindings.Add($role);
		$web.RoleAssignments.Add($assignment);
	}
}

function AssignRoleToPrincipalInList([Microsoft.SharePoint.SPList]$list, [Microsoft.SharePoint.SPPrincipal]$principal, 
	[Microsoft.SharePoint.SPRoleType]$roleType, [bool]$removeExistingRoles)
{
	if ($principal -ne $null)
	{
		if (-not $list.HasUniqueRoleAssignments) {
			$list.BreakRoleInheritance($true)
		}
		
		if ($removeExistingRoles)
		{
			$assignmentToRemove = $null
			try {
				$assignmentToRemove = $list.RoleAssignments.GetAssignmentByPrincipal($principal);
			}
			catch {}
			if ($assignmentToRemove -ne $null) {
				$assignmentToRemove.RoleDefinitionBindings.RemoveAll();
				$assignmentToRemove.Update();
			}
		}

		$assignment = new-object Microsoft.SharePoint.SPRoleAssignment($principal);
		$role = $list.ParentWeb.RoleDefinitions.GetByType($roleType);
		$assignment.RoleDefinitionBindings.Add($role);
		$list.RoleAssignments.Add($assignment);
	}
}

function EncodeClaimsLogin2([string]$loginName)
{
	$principal = New-SPClaimsPrincipal -Identity $loginName -IdentityType "WindowsSamAccountName"
	([Microsoft.SharePoint.Administration.Claims.SPClaimProviderManager]::Local).EncodeClaim($principal)
}

function EncodeClaimsLogin([string]$siteUrl, [string]$loginName)
{
	$site = Get-SPSite $siteUrl -ea SilentlyContinue
	if ($site -ne $null) {
		$webApplication = $site.WebApplication
		$zone = $site.Zone
		$encodedLogin = ([Microsoft.SharePoint.Utilities.SPUtility]::ResolvePrincipal($webApplication, $zone, $loginName, [Microsoft.SharePoint.Utilities.SPPrincipalType]::All, [Microsoft.SharePoint.Utilities.SPPrincipalSource]::All, $false)).LoginName
		$encodedLogin
	}
	else {
		$loginName
	}
}

function Trim-UrlFinalSlash([string]$url)
{
	if ($url.EndsWith("/")) {
		return $url.Substring(0, $url.Length - 1)
	}
	else {
		return $url
	}
}

function Get-SafeUrl([string]$url)
{
	return Trim-UrlFinalSlash $url
}

function Check-ValidUrl([string]$url)
{
	return ([System.Uri]::IsWellFormedUriString($url, [System.UriKind]::Absolute));
}

function Check-ValidSiteCollection([string]$url)
{
	LoadSharePoint
	return (([System.Uri]::IsWellFormedUriString($url, [System.UriKind]::Absolute)) -and ((Get-SPSite $url -ea SilentlyContinue) -ne $null));
}

function Check-ValidWeb([string]$url)
{
	LoadSharePoint
	return (([System.Uri]::IsWellFormedUriString($url, [System.UriKind]::Absolute)) -and ((Get-SPWeb $url -ea SilentlyContinue) -ne $null));
}

function Check-ValidWebApplication([string]$url)
{
	LoadSharePoint
	return (([System.Uri]::IsWellFormedUriString($url, [System.UriKind]::Absolute)) -and ((Get-SPWebApplication $url -ea SilentlyContinue) -ne $null));
}

function Check-ValidUser([string]$username)
{
	if ($username.Contains("|")) {
		return $true;
	}
	if (-not $username.Contains("\")) {
		return $false;
	}
	else {
		try {
			Import-Module ActiveDirectory -ErrorAction SilentlyContinue
			$domain = $username.Substring(0, $username.IndexOf("\"))
			$user = $username.Replace($domain + "\", "")
			if (Get-Command "Get-ADUser" -ErrorAction SilentlyContinue) {
				return ((Get-ADUser -Filter { (ObjectClass -eq "User") -and (samAccountName -eq $user) }) -ne $null)
			}
			else {
				return $true
			}
		}
		catch {
			return $true
		}
	}
}

function LoadSharePoint()
{
	if (-not(Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ea SilentlyContinue)) { 
		Add-PSSnapin "Microsoft.SharePoint.PowerShell" 
	}
}

function Restart-IIS()
{
	Write-InstallStep "Restarting IIS..."
	iisreset /noforce 2>&1 | Out-Null
	$iis = get-wmiobject Win32_Service -Filter "name='IISADMIN'"
	if($iis.State -ne "Running") {
		iisreset 2>&1 | Out-Null
	}
	Write-InstallStepDone
}

function Restart-SPTimer()
{
	Write-InstallStep "Restarting SPTimer process..."
	try {
		net stop sptimerv4 > $null
	} catch { }
	try {
		net start sptimerv4 > $null
	} catch { }
	Write-InstallStepDone
}

function New-Collection([string] $typeName = $(throw "Please specify a generic type name"), 
						[string[]] $typeParameters = $(throw "Please specify the type parameters"), 
						[object[]] $constructorParameters) 
{

	# Create the generic type name
	$genericTypeName = $typeName + '`' + $typeParameters.Count
	$genericType = [Type] $genericTypeName

	if(-not $genericType)
	{
		throw "Could not find generic type $genericTypeName"
	}

	# Bind the type arguments to it
	[type[]] $typedParameters = $typeParameters

	$closedType = $genericType.MakeGenericType($typedParameters)
	if(-not $closedType)
	{
		throw "Could not make closed type $genericType"
	}

	# Create the closed version of the generic type
	,[Activator]::CreateInstance($closedType, $constructorParameters)
}

function Upload-File([string]$FilePath, [string]$FolderUrl) 
{
	$fileBytes = [System.IO.File]::ReadAllBytes($FilePath)
	$fileInfo = new-object System.IO.FileInfo($FilePath)
	$fileName = $fileInfo.Name

	$site = new-object Microsoft.SharePoint.SPSite($FolderUrl)
	$web = $site.OpenWeb()

	$folder = $web.GetFolder($FolderUrl)
	$folder.Files.Add(($web.Url + "/" + $folder.Url + "/" + $fileName), $fileBytes) > $null

	$web.Dispose()
	$site.Dispose()
}

function Restore-Permission-Inheritance([string]$WebURL, [string]$ListName)
{
	$site = new-object Microsoft.SharePoint.SPSite($WebURL)
	$web = $site.OpenWeb()

	$list = $web.Lists[$ListName]
	if ($list.HasUniqueRoleAssignments) {
		$list.ResetRoleInheritance()
	}
	
	$web.Dispose()
	$site.Dispose()
}

function Get-SPDatabaseHost([string]$SiteCollectionUrl)
{
	LoadSharePoint
	$farm = Get-SPFarm
	$farmBuilder = new-object Microsoft.SharePoint.Administration.SPWebApplicationBuilder($farm)
	$DbServer = $farmBuilder.DatabaseServer
	if (($DbServer -eq $null) -or ($DbServer -eq "")) {
		$DbServer = $env:COMPUTERNAME
	}
	
	return $DbServer
}

function DeleteBeezyDatabase([string]$SiteCollectionUrl)
{
	Add-Type -AssemblyName "Spenta.Beezy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=784c41e972d0ad71" > $null
	Add-Type -AssemblyName "Spenta.Beezy.Data.Sql, Version=1.0.0.0, Culture=neutral, PublicKeyToken=784c41e972d0ad71" > $null
	
	[Microsoft.SharePoint.SPSecurity]::RunWithElevatedPrivileges( {

		$site = Get-SPSite($SiteCollectionUrl)
		$web = $site.OpenWeb()
		$connectionString = $web.AllProperties["PropertyBags:Beezy;ConnectionString"]
		
		([Spenta.Beezy.Factories.DatabaseBuilderFactory]::CreateInstance()).Delete($connectionString)
	} )
}

function Get-BeezyRestSharpApi([string]$SiteCollectionUrl)
{
	Add-Type -Path .\RestSharp.dll > $null
	Add-Type -AssemblyName "Spenta.Beezy.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=784c41e972d0ad71" > $null
	Add-Type -Path "Spenta.Beezy.Client.dll" > $null
	$serviceUrl = $SiteCollectionUrl + "/_vti_bin/Beezy/v2/Api.svc"
	$restClient = new-object RestSharp.RestClient($serviceUrl) 
	$restClient.Authenticator = new-object RestSharp.NtlmAuthenticator
	$adminServiceUrl = $SiteCollectionUrl + "/_vti_bin/Beezy/v2/Administration.svc"
	$adminRestClient = new-object RestSharp.RestClient($adminServiceUrl) 
	$adminRestClient.Authenticator = new-object RestSharp.NtlmAuthenticator
	$beezyRestSharpApi = new-object Spenta.Beezy.Client.Api.BeezyRestSharpApi($restClient, $adminRestClient)
	return $beezyRestSharpApi
}

function Get-LogFile()
{
	return ".\Log-" + ((Get-Date -format d).Replace("/", "-")) + ".txt"
}

function Get-Lines($message) 
{
	if ($message -eq "") {
		return $message
	}
	$lines = @()
	$windowWidth = (Get-Host).UI.RawUI.WindowSize.Width
	if ($windowWidth -eq $null) {
		$windowWidth = 80
	}
	$width = $windowWidth - 23
	for ($i=0; $i -lt $message.Length; $i+=$width) {
		$lines += $message.Substring($i, [System.Math]::Min($width, $message.Length - $i))
	}
	$lines
}

function Write-Host-Timestamp($prefix = "", $message, [string]$Foreground = $null, [Switch]$NoNewLine) 
{
	foreach ($text in Get-Lines($message)) {
		$text = ("[" + (Get-Date -format 'dd/MM/yyyy').Substring(0, 6) + (Get-Date -format 'dd/MM/yyyy').Substring(8, 2) + " " + (Get-Date -format T) + "] ") + $prefix + $text
		if (-not $NoNewLine) {
			$text += "`r"
		}
		$params = @{} 
		if ($NoNewLine) { $params.Add("NoNewLine", $true) }
		if (($Foreground -ne $null) -and ($Foreground -ne "")) { $params.Add("ForegroundColor", $Foreground) }
		Write-Host @params $text
	}
}

function Write-InstallHeader()
{
	Write-InstallBlankLine
	Write-Host-Timestamp "" "" -NoNewLine
	Write-Host " " -NoNewLine -Back Blue
	Write-Host " " -NoNewLine
	Write-Host " " -NoNewLine -Back Yellow
	Write-Host " " -NoNewLine
	Write-Host " " -NoNewLine -Back Magenta
	Write-Host " " -NoNewLine
	Write-Host " " -NoNewLine -Back Green
	Write-Host " Beezy Install (c) 2014 " -NoNewLine -Fore White
	Write-Host " " -NoNewLine -Back Green
	Write-Host " " -NoNewLine
	Write-Host " " -NoNewLine -Back Magenta
	Write-Host " " -NoNewLine
	Write-Host " " -NoNewLine -Back Yellow
	Write-Host " " -NoNewLine
	Write-Host " " -NoNewLine -Back Blue
	Write-Host " "
}

function Write-InstallFooter()
{
	Write-InstallBlankLine
}

function Write-InstallSection([string]$message) 
{
	Write-InstallBlankLine
	Write-Host-Timestamp "" "--- $message ---" -Foreground Cyan
	Write-InstallBlankLine
}

function Write-InstallSuccessSection([string]$message)
{
	Write-InstallBlankLine
	Write-Host-Timestamp "" "--- $message ---" -Foreground Green
	Write-InstallBlankLine
}

function Write-InstallStep([string]$message)
{
	Write-Host-Timestamp "" "> $message"
}

function Write-InstallStepDone()
{
	Write-InstallMessage "Done."
}

function Write-InstallMessage([string]$message)
{
	Write-Host-Timestamp "  " $message
}

function Write-InstallSuccessMessage([string]$message)
{
	Write-Host-Timestamp "  " $message -Foreground Green
}

function Write-InstallError([string]$message)
{
	Write-Host-Timestamp "  " $message -Foreground Red
}

function Write-InstallWarning([string]$message)
{
	Write-Host-Timestamp "  " $message -Foreground Yellow
}

function Write-InstallBlankLine()
{
	Write-Host-Timestamp "" ""
}

function Get-HostVersion()
{
	(Get-Host).Version.ToString()
}

function Invoke-FlashWindow()
{
	Add-Type -TypeDefinition @"
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Runtime.InteropServices;

	public class Window
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct FLASHWINFO
		{
			public UInt32 cbSize;
			public IntPtr hwnd;
			public UInt32 dwFlags;
			public UInt32 uCount;
			public UInt32 dwTimeout;
		}

		//Stop flashing. The system restores the window to its original state. 
		const UInt32 FLASHW_STOP = 0;
		//Flash the window caption. 
		const UInt32 FLASHW_CAPTION = 1;
		//Flash the taskbar button. 
		const UInt32 FLASHW_TRAY = 2;
		//Flash both the window caption and taskbar button.
		//This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
		const UInt32 FLASHW_ALL = 3;
		//Flash continuously, until the FLASHW_STOP flag is set. 
		const UInt32 FLASHW_TIMER = 4;
		//Flash continuously until the window comes to the foreground. 
		const UInt32 FLASHW_TIMERNOFG = 12; 


		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

		public static bool FlashWindow(IntPtr handle, UInt32 timeout, UInt32 count)
		{
			IntPtr hWnd = handle;
			FLASHWINFO fInfo = new FLASHWINFO();

			fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
			fInfo.hwnd = hWnd;
			fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
			fInfo.uCount = count;
			fInfo.dwTimeout = timeout;

			return FlashWindowEx(ref fInfo);
		}
	}
"@
	
	$process = Get-Process -Id $pid
	$handle = $process.MainWindowHandle
	$result = [window]::FlashWindow($handle,150,10)
}