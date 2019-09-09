# Export SharePoint list
Export-SPWeb -Identity "http://sp.dev/subsite" -ItemUrl "/subsite/lists/List Title here" -path "c:\temp\tempfile.txt" -IncludeVersions –includeusersecurity

# Export SharePoint site
Export-SPWeb "http://sp.dev/subsite" -Path "c:\temp\site export.cmp"

# Import SharePoint list
Import-SPWeb -Path "c:\temp\listbackupfile.txt" -UpdateVersions Overwrite

# Import SharePoint site
Import-SPWeb "http://sp.dev/subsite" -Path "c:\temp\site export.cmp" -UpdateVersions Overwrite