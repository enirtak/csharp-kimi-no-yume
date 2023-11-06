using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models.JobApplication;
using proj_csharp_kiminoyume.Services.JobApplication;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class JobApplicationBusinessLogic : IJobApplicationBusinessLogic
    {
        private readonly AppDBContext _context;

        public JobApplicationBusinessLogic(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplication>> GetJobApplicationList()
        {
            var jobAppList = new List<JobApplication>();

            try
            {
                jobAppList = await _context.JobApplications
                    .Where(x => x.IsActive)
                    .Include(x => x.CustomFields.Where(x => x.IsActive))
                    .ToListAsync();
            }
            catch
            {
                throw;
            }

            return jobAppList;
        }

        public async Task<JobApplication?> CreateNewJobApplication(JobApplication request)
        {
            if (request == null) return null;

            try
            {
                var newJobApplication = _context.JobApplications.Add(request);
                await _context.SaveChangesAsync();

                return newJobApplication?.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<JobApplication?> UpdateJobApplication(JobApplication request)
        {
            if (request == null) return null;

            try
            {
                var currentEntry = await _context.JobApplications
                    .Include(x => x.CustomFields)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (currentEntry != null)
                {
                    var updatedEntry = _context.Entry(currentEntry);
                    updatedEntry.CurrentValues.SetValues(request);

                    // Update Child Entity
                    if (request.CustomFields != null && request.CustomFields.Count > 0)
                    {
                        UpSertEntityHelper<JobAppCustomField>
                           .UpSertEntities(_context, request.CustomFields, currentEntry.CustomFields,
                           (newEntity, old) => { return newEntity.Id == old.Id && old.JobApplicationId == currentEntry.Id; });
                    }

                    updatedEntry.State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return updatedEntry?.Entity;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteJobApplication(int id)
        {
            if (id == default) return;

            try
            {
                await _context.JobApplications
                    .Where(x => x.Id == id)
                    .ExecuteUpdateAsync(update => update.SetProperty(up => up.IsActive, false));
            }
            catch
            {
                throw;
            }
        }
    }
}
