# Loads the current cache cluster
use-cachecluster

# Gets the cache host
get-cachehost

# Gets the cache host settings
get-cachehostconfig hostname 22233

# Gets the accounts associated with the cache host
Get-CacheAllowedClientAccounts

# Gets all the cache instances
get-cache

# Gets the configuration on a specific cache instance
get-cacheconfig DistributedActivityFeedCache_aa1c2ab2-5764-46d6-b448-21f51124e9e9

# Restarts and clears all of the cache
Restart-CacheCluster

# Displays the cache health
Get-CacheClusterHealth