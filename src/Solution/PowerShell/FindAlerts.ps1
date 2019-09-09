Add-PSSnapin “Microsoft.SharePoint.PowerShell”

foreach($SpWebApp in Get-SPWebApplication)
{
    Write-host 'Site: ' $SpWebApp.Url
    foreach ($SpSite in $SpWebApp.Sites)
    {
        foreach($SpWeb in $SpSite.AllWebs)
        {
            foreach($alert in $SpWeb.Alerts)
            {
                foreach ($usr in $alert.User)
                {
                    If ($alert.User.Email -eq 'email@addresstofind.com')
                    {
                        Write-host 'Email: ' $alert.User.Email
                        Write-host 'Title: ' $alert.Title
                        Write-host 'List Url: ' $SpWeb.Url'/'$alert.ListUrl
                        Write-host ''
                    }
                }  
            }
        }
    }
}