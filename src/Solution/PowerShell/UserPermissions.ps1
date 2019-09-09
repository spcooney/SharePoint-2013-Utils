# Add user as a secondary site collection admin
Set-SPSite -Identity "http://webapp/sites/somesite" -SecondaryOwnerAlias 'domain\username'

# Add user to a group by the group ID
Set-SPUser -Identity 'domain\username' -Web "http://webapp/sites/somesite" -Group 4