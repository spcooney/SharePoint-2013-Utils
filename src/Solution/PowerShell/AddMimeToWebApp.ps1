# Adds the MIME type "application/font-woff2" to the web application
$webApplication = Get-SPWebApplication "http://webapp"
# Gets all the MIME types
#$webApplication.AllowedInlineDownloadedMimeTypes
$webApplication.AllowedInlineDownloadedMimeTypes.Add("application/font-woff2")
$webApplication.Update()