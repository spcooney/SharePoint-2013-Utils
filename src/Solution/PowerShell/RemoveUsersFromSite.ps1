#============================================================#
# WARNING: THIS SCRIPT WILL REMOVE ALL THE USERS FROM A SITE #
#============================================================#
$site = new-object Microsoft.SharePoint.SPSite ( "http://webapp/sites" )
$web = $site.OpenWeb()
"Web is : " + $web.Title
$oSiteGroup = $web.SiteGroups
# Loop through all the groups in the site
foreach ($curGroup in $oSiteGroup)
{
	Write-Host "Current group: " $curGroup
	# Loop through all the users in the group
	foreach ($oUser in $curGroup.Users)
	{
		Write-Host "Current user: " $oUser.Name
		# Remove the current user
		$curGroup.RemoveUser($oUser)
	} 
}