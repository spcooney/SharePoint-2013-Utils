Write-Host "Cataloging Features..." -foreground Blue
$featureFiles = Get-ChildItem -include "*.feature" -recurse
ForEach($featureFile in $featureFiles)
{
	[xml]$x = Get-Content $featureFile
	Write-Host "===FEATURE===Id:" $x.feature.featureId "Scope:" $x.feature.scope -foreground Cyan
	Write-Host "Title: " $x.feature.title
	Write-Host "Description: " $x.feature.description
	Write-Host "   Hidden? " $x.feature.isHidden " AutoActivate? " $x.feature.activateOnDefault " ForceInstall? " $x.feature.alwaysForceInstall
	Write-Host "   Has Receiver? " $x.feature.receiverClass
}