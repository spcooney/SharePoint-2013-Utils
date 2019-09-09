##########################################################################################  
# Split a log/text file into smaller chunks 						 # 
# https://gallery.technet.microsoft.com/scriptcenter/PowerShell-Split-large-log-6f2c4da0 #
##########################################################################################  
# 
# WARNING: This will take a long while with extremely large files and uses lots of memory to stage the file 
# 
 
# Set the baseline counters 
# 
# Set the line counter to 0 
$linecount = 0 
# Set the file counter to 1. This is used for the naming of the log files 
$filenumber = 1 
 
# Prompt user for the path 
$sourcefilename = Read-Host "What is the full path and name of the log file to split? (e.g. D:\mylogfiles\mylog.txt)" 
 
# Prompt user for the destination folder to create the chunk files 
$destinationfolderpath = Read-Host "What is the path where you want to extract the content? (e.g. d:\yourpath\)" 
 
Write-Host "Please wait while the line count is calculated. This may take a while. No really, it could take a long time." 
 
# Find the current line count to present to the user before asking the new line count for chunk files 
Get-Content $sourcefilename | Measure-Object | ForEach-Object { $sourcelinecount = $_.Count } 
 
#Tell the user how large the current file is 
Write-Host "Your current file size is $sourcelinecount lines long" 
 
# Prompt user for the size of the new chunk files 
$destinationfilesize = Read-Host "How many lines will be in each new split file?" 
 
# the new size is a string, so we convert to integer and up 
# Set the upper boundary (maximum line count to write to each file) 
$maxsize = [int]$destinationfilesize  
 
Write-Host File is $sourcefilename - destination is $destinationfolderpath - new file line count will be $destinationfilesize 
 
# The process reads each line of the source file, writes it to the target log file and increments the line counter. When it reaches 100000 (approximately 50 MB of text data) 
$content = get-content $sourcefilename | % { 
 Add-Content $destinationfolderpath\splitlog$filenumber.txt "$_" 
  $linecount ++ 
  If ($linecount -eq $maxsize) { 
    $filenumber++ 
	Write-Host "splitlog$filenumber.txt complete"
    $linecount = 0 
  } 
} 
 
# Clean up after your pet 
[gc]::collect()  
[gc]::WaitForPendingFinalizers() 