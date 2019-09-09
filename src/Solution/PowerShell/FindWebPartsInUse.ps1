$webAppUrl = "http://webapp"
$webPartName = "WebPartName"
$webApplication = Get-SPWebApplication $webAppUrl
foreach($site in $webApplication.Sites)
{  
	foreach($web in $site.AllWebs) 
	{
		if ([Microsoft.SharePoint.Publishing.PublishingWeb]::IsPublishingWeb($web)) 
		{
			$pWeb = [Microsoft.SharePoint.Publishing.PublishingWeb]::GetPublishingWeb($web)
			$pages = $pWeb.PagesList
			foreach ($item in $pages.Items) 
			{
				$fileUrl = $webUrl + "/" + $item.File.Url
				$manager = $item.file.GetLimitedWebPartManager([System.Web.UI.WebControls.Webparts.PersonalizationScope]::Shared);
				$wps = $manager.webparts
				foreach($webpart in $wps)
				{
					if ($webpart | Where {$_.Title -eq $webPartName })
					# if($webPartName -eq $webpart.GetType().ToString())
					{
						Write-Host "Found" $webpart.GetType().ToString() "at" $web.Url -BackgroundColor Green -ForegroundColor Black
					}
				}
			}
		}
		else
		{
			$pages = $null
			$pages = $web.Lists["Site Pages"]            
			if ($pages) 
			{                
				foreach ($item in $pages.Items) 
				{
					$fileUrl = $webUrl + "/" + $item.File.Url
					$manager = $item.file.GetLimitedWebPartManager([System.Web.UI.WebControls.Webparts.PersonalizationScope]::Shared);
					$wps = $manager.webparts
					foreach($webpart in $wps)
					{
						if ($webpart | Where {$_.Title -eq $webPartName })
						{
							Write-Host "Found" $webpart.GetType().ToString() "at" $web.Url -BackgroundColor Green -ForegroundColor Black
						}
					}
				}
			}
		}  
		$web.Dispose()  
	}
	$site.Dispose()
}