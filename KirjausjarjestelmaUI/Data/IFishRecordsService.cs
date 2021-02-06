using KirjausjarjestelmaUI.Data.BlazorApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KirjausjarjestelmaUI.Data
{
    public interface IFishRecordsService
    {
        Task<List<FishRecords>> GetAllFishRecordsAsync();
        Task<List<FishRecords>> GetUserFishRecordsAsync(string strCurrentUser);
        Task<FishRecords> CreateFishRecordsAsync(FishRecords objFishRecords);
        Task<bool> UpdateFishRecordsAsync(FishRecords objFishRecords);
        Task<bool> DeleteFishRecordsAsync(Guid Id);
    }
}
