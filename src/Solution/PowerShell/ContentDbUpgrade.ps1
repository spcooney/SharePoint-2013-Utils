# Mount the content database to a web application
Mount-SPContentDatabase -webapplication http://webapp -name "WSS_Content_DB"

# Test the content database to see if there are any issues
Test-SPContentDatabase -Name WSS_Content_DB -WebApplication http://webapp/site/testsite

# Upgrade the site collection to the next version
Upgrade-SPSite http://webapp/site/testsite -versionupgrade

Upgrade-SPContentDatabase WSS_Content_DB

# Creates new content database
New-SPContentDatabase -Name WSS_Content_DB -DatabaseServer DbServerName -WebApplication http://webapp/site/testsite

# Creates a new site in the new content database
New-SPSite -URL http://webapp/site/testsite -OwnerAlias 'Domain\Username' -ContentDatabase WSS_Content_DB -CompatibilityLevel 14