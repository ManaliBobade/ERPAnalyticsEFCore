using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

public class CountDataService
{
    private readonly ApplicationDBContext _context;

    public CountDataService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task SaveCountDataAsync(IEnumerable<CountData> countDataList)
    {
        // Assuming you have a CountData table in your DB context
        await _context.CountData.AddRangeAsync(countDataList);
        await _context.SaveChangesAsync();
    }

    public async Task<CountTotals> GetCountTotalsFilteredAsync(
    List<string> cameraNames,
    DateTime inAfter,
    DateTime outBefore)
    {
        int inAfterUnix = (int)((DateTimeOffset)inAfter).ToUnixTimeSeconds();
        int outBeforeUnix = (int)((DateTimeOffset)outBefore).ToUnixTimeSeconds();

        var totals = await _context.CountData
            .Where(cd =>
                cameraNames.Contains(cd.CameraName) &&
                cd.In > inAfterUnix &&
                cd.Out < outBeforeUnix)
            .GroupBy(_ => 1)
            .Select(g => new CountTotals
            {
                TotalIn = g.Sum(cd => cd.In),
                TotalOut = g.Sum(cd => cd.Out)
            })
            .FirstOrDefaultAsync();

        return totals ?? new CountTotals(); // Return 0s if no match
    }

    public class CountTotals
    {
        public int TotalIn { get; set; }
        public int TotalOut { get; set; }

        // Optional: enable deconstruction
        public void Deconstruct(out int totalIn, out int totalOut)
        {
            totalIn = this.TotalIn;
            totalOut = this.TotalOut;
        }
    }



}