# Get the site
$sc = Get-SPSite http://webapp/sites
# Sets a variable for the site collection
$sc.CompatibilityLevel
# Returns the compatibility level for the site collection (either 14 or 15 for 2010 or 2013 mode)
$sc.UpgradeInfo
# Returns the upgrade information for the site collection

#Short version
(Get-SPSite http://webapp/sites).CompatibilityLevel
(Get-SPSite http://webapp/sites).UpgradeInfo