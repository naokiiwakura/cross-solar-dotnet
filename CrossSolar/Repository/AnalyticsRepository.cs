using CrossSolar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public class AnalyticsRepository : GenericRepository<OneHourElectricity>, IAnalyticsRepository
    {
        public AnalyticsRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OneHourElectricity>> ReturnOneHourElectricity(string serial)
        {
            return await _dbContext.OneHourElectricitys.Where(x => x.PanelId.Equals(serial, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();                
        }

        public async Task<List<OneHourElectricity>> DayResults(string panelId)
        {
            return await _dbContext.OneHourElectricitys.Where(x => x.PanelId.Equals(panelId, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
                           //group ana by new { y = ana.DateTime.Year, m = ana.DateTime.Month, d = ana.DateTime.Day } into g
                           //select new OneDayElectricityModel
                           //{
                           //    Sum = g.Sum(x => x.KiloWatt),
                           //    Average = g.Average(x => x.KiloWatt),
                           //    Maximum = g.Max(x => x.KiloWatt),
                           //    Minimum = g.Min(x => x.KiloWatt),
                           //    DateTime = g.FirstOrDefault().DateTime.Date
                           //}).ToListAsync();
        }
    }
}