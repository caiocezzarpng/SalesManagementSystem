using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var cacheKey = $"simple-{minDate:yyyyMMdd}-{maxDate:yyyyMMdd}";
            var cachedRecords = SalesRecordCache.Instance.Get(cacheKey);

            if (cachedRecords != null && cachedRecords.Any())
            {
                return cachedRecords;
            }

            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            var salesRecords = await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            SalesRecordCache.Instance.Set(cacheKey, salesRecords);
            return salesRecords;
        }

        public async Task<List<IGrouping<Department?, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var cacheKey = $"group-{minDate:yyyyMMdd}-{maxDate:yyyyMMdd}";
            var cachedGroupedRecords = SalesRecordCache.Instance.GetGrouped(cacheKey);

            if (cachedGroupedRecords.Any())
            {
                return cachedGroupedRecords;
            }

            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            var data = await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            var groupedRecords = data.GroupBy(s => s.Seller.Department).ToList();
            SalesRecordCache.Instance.SetGrouped(cacheKey, groupedRecords);
            return groupedRecords;
        }
    }
}