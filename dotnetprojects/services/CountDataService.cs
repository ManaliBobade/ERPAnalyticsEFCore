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

}