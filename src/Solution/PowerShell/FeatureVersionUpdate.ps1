Param
(
    $site = "http://webapp/sites/testsite/",
	$FeatureName = "FULLFEATURENAME"
)
# Load sharepoint powershell if not already loaded
$spSnapIn = Get-PSSnapIn | where {$_.Name -eq "Microsoft.SharePoint.PowerShell"}
if( $spSnapIn -eq $null )
{
    Add-PSSnapIn Microsoft.SharePoint.PowerShell
}
# Get the sharepoint web
$web = get-spweb $site
# Get our feature
$feature = $web.features | where {$_.Definition.DisplayName -match $FeatureName}
if(-not($feature))
{
    Write-Host "Feature $FeatureName not found"
}
else
{
    # Does it need an upgrade?
    if($feature.Version -lt $feature.Definition.Version)
    {
        Write-Host "Upgrading $feature.Definition.DisplayName from" $feature.Version "to" $feature.Definition.Version
        $feature.Upgrade($false)
    }
    else
    {
        Write-Host "$feature.Definition.DisplayName is already up to date at" $feature.Version
    }
}