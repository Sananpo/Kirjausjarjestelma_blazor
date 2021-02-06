using KirjausjarjestelmaDB.Data.BlazorApp;
using KirjausjarjestelmaUI.Data.BlazorApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KirjausjarjestelmaUI.Data
{
    public class FishRecordsService : IFishRecordsService
    {
        private readonly BlazorappDevContext context;

        public FishRecordsService(BlazorappDevContext context)
        {
            this.context = context;
        }

        public Task<FishRecords> CreateFishRecordsAsync(FishRecords objFishRecords)
        {
            context.FishRecords.Add(objFishRecords);
            context.SaveChanges();
            return Task.FromResult(objFishRecords);
        }

        public Task<bool> DeleteFishRecordsAsync(Guid Id)
        {
            var ExistingFishRecord =
                context.FishRecords
                .Where(x => x.Id == Id)
                .FirstOrDefault();
            if (ExistingFishRecord != null)
            {
                context.FishRecords.Remove(ExistingFishRecord);
                context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task<List<FishRecords>> GetAllFishRecordsAsync()
        {
            return await context.FishRecords
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<FishRecords>> GetUserFishRecordsAsync(string strCurrentUser)
        {
            return await context.FishRecords
                .Where(x => x.UserName == strCurrentUser)
                .AsNoTracking().ToListAsync();
        }

        public Task<bool> UpdateFishRecordsAsync(FishRecords objFishRecords)
        {
            var ExistingFishRecord =
                context.FishRecords
                .Where(x => x.Id == objFishRecords.Id)
                .FirstOrDefault();
            if (ExistingFishRecord != null)
            {
                ExistingFishRecord.Weight =
                    objFishRecords.Weight;
                ExistingFishRecord.Latitude =
                    objFishRecords.Latitude;
                ExistingFishRecord.Length =
                    objFishRecords.Length;
                ExistingFishRecord.Type =
                    objFishRecords.Type;
                context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
