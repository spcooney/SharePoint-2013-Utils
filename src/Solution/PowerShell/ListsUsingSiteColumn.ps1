$web = Get-SPWeb "http://webapp/sites"
$column = $web.Fields["HashTags"]
[array]$lists = $column.ListsFieldUsedIn()
foreach ($list in $lists)
{
	#Write-Output $list.ListID
	Get-SPSite "http://webapp/sites" | Get-SPWeb  -Limit ALL | %{$_.Lists} | ?{$_.ID –eq $list.ListID} | ft Title, ParentWebURL, RootFolder
}