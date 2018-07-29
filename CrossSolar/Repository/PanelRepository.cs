using CrossSolar.Domain;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrossSolar.Repository
{
    public class PanelRepository : GenericRepository<Panel>, IPanelRepository
    {
        public PanelRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Panel> GetPanelBySerial(string serial)
        {
            return await _dbContext.Panels.Where(p => p.Serial.Equals(serial, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefaultAsync();
        }
    }
}