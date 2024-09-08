using SalesManagementSystem.Models;

public class SalesRecordCache
{
    private static SalesRecordCache _instance;
    private static readonly object _lock = new object();
    private Dictionary<string, List<SalesRecord>> _cache;
    private Dictionary<string, List<IGrouping<Department?, SalesRecord>>> _groupedCache;

    private SalesRecordCache()
    {
        _cache = new Dictionary<string, List<SalesRecord>>();
        _groupedCache = new Dictionary<string, List<IGrouping<Department?, SalesRecord>>>();
    }

    public static SalesRecordCache Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SalesRecordCache();
                    }
                }
            }
            return _instance;
        }
    }

    public List<SalesRecord>? Get(string key)
    {
        _cache.TryGetValue(key, out var salesRecords);
        return salesRecords;
    }

    public void Set(string key, List<SalesRecord> salesRecords)
    {
        _cache[key] = salesRecords;
    }

    public List<IGrouping<Department?, SalesRecord>>? GetGrouped(string key)
    {
        _groupedCache.TryGetValue(key, out var groupedRecords);
        return groupedRecords ?? new List<IGrouping<Department?, SalesRecord>>();
    }

    public void SetGrouped(string key, List<IGrouping<Department?, SalesRecord>> groupedRecords)
    {
        _groupedCache[key] = groupedRecords;
    }
}