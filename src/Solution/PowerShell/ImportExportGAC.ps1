#Export
Set-location "c:\Folder Path"            
[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")            
$publish = New-Object System.EnterpriseServices.Internal.Publish            
$publish.GacRemove("c:\Folder Path\DLL.dll")  

# Import
Set-location "c:\Folder Path"            
[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")            
$publish = New-Object System.EnterpriseServices.Internal.Publish            
$publish.GacInstall("c:\Folder Path\DLL.dll")

# Import (Alternative)
[Reflection.Assembly]::LoadWithPartialName("AssemblyName") | Out-Null 
[System.EnterpriseServices.Internal.Publish] $publish = new-object System.EnterpriseServices.Internal.Publish
$publish.GacInstall("c:\Folder Path\DLL.dll")