$farm = Get-SPFarm

foreach ($prop in $farm.Properties.GetEnumerator()) {
	if ($prop.name -eq "PropertyName" -or $prop.name -eq "PropertyName2") {
		write-host "name: " $prop.name " value: " $prop.value
	}
}