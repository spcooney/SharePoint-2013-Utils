# Find orphan features
Get-SPFeature | ? { $_.Scope -eq $null }

# Delete orphan feature by name
$feature = Get-SPFeature | ? { $_.DisplayName -eq "My_Orphane_Feature" }
$feature.Delete()

# Delete all orphaned features
Get-SPFeature | ? { !$_.Scope } | % { $_.Delete() }