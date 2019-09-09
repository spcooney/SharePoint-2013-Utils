Param
(
    $site = "http://webapp/sites/testsite/"
)
# Load sharepoint powershell if not already loaded
$spSnapIn = Get-PSSnapIn | where {$_.Name -eq "Microsoft.SharePoint.PowerShell"}
if( $spSnapIn -eq $null )
{
    Add-PSSnapIn Microsoft.SharePoint.PowerShell
}
# Get the sharepoint web
$web = get-spweb $site
if( $filter ) 
{ 
    $features = $web.features | where {$_.Definition.DisplayName -match $filter} 
}
else 
{ 
    $features = $web.features 
}
$features | select @{Name="Feature";Expression={$_.Definition.DisplayName}}, Version, @{Name="Feature Version";Expression={$_.Definition.Version}}