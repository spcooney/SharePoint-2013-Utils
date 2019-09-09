Write-Host "Cataloging Features..." -foreground Blue
$featureFiles = Get-ChildItem -include "feature.xml" -recurse
ForEach($featureFile in $featureFiles)
{
	[xml]$x = Get-Content $featureFile
	if ($x.feature.Title -like '*Resources:*') {	continue;	}  #ignore Microsoft stuff
	if ($x.feature.Description -like '*Resources:*') {	continue;	}  #ignore Microsoft stuff
	if ($x.feature.ReceiverAssembly -like 'Microsoft.*') {	continue;	}  #ignore Microsoft stuff
	Write-Host "===FEATURE===Id:" $x.feature.Id "Scope:" $x.feature.scope -foreground Cyan
	Write-Host "Title: " $x.feature.title
	Write-Host "Description: " $x.feature.description
	Write-Host "   Hidden? " $x.feature.Hidden " AutoActivate? " $x.feature.activateOnDefault " ForceInstall? " $x.feature.alwaysForceInstall
	Write-Host "   ReceiverAssembly: " $x.feature.receiverAssembly
	Write-Host "   ReceiverClass: " $x.feature.receiverClass
	Write-Host "   SolutionId: " $x.feature.solutionId
	Write-Host "   DefaultResourceFile: " $x.feature.DefaultResourceFile
	Write-Host "   xmlns: " $x.feature.xmlns
	Write-Host "   Directory: " $featureFile.Directory.ToString().ToLowerInvariant().Replace(
		"c:\program files\common files\microsoft shared\web server extensions\","");
}