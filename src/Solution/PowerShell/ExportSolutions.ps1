Add-PSSnapin "Microsoft.SharePoint.Powershell"

# Make sure this path exists or else it won't export
$dirName = "E:\Export\Solutions"
Write-Host Exporting solutions to $dirName
foreach ($solution in Get-SPSolution)
{
    $id = $Solution.SolutionID
    $title = $Solution.Name
    $filename = $Solution.SolutionFile.Name
    Write-Host "Exporting ‘$title’ to …\$filename" -nonewline
    try
    {
        $solution.SolutionFile.SaveAs("$dirName" + "\" + "$filename")
        Write-Host " – done" -foreground green
    }  
    catch  
    {  
        Write-Host " – error : $_" -foreground red
    }
}