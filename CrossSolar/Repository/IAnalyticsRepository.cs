using CrossSolar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IAnalyticsRepository : IGenericRepository<OneHourElectricity>
    {
        Task<List<OneHourElectricity>> ReturnOneHourElectricity(string serial);
        Task<List<OneHourElectricity>> DayResults(string panelId);
    }
}