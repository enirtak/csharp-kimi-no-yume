using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services.Profile;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class ProfileBusinessLogic : IProfileBusinessLogic
    {
        private readonly AppDBContext _context;

        public ProfileBusinessLogic(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Person?> CreateNewProfile(Person request)
        {
            try
            {
                if (request != null)
                {
                    var newProfile = _context.Persons.Add(request);
                    await _context.SaveChangesAsync();
                    return newProfile?.Entity;
                }

                return null;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<List<Person>> GetProfileList(bool getAll = false)
        {
            try
            {
                var profile = await _context.Persons
                    .Where(x => x.IsActive == getAll)
                        .Include(x => x.Addresses
                            .Where(y => y.IsActive == getAll))
                        .Include(x => x.Employers
                            .Where(x => x.IsActive == getAll))
                                .ThenInclude(x => x.WorkExperience
                                    .Where(x => x.IsActive == getAll))
                    .AsNoTracking()
                    .ToListAsync();


                return profile;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Person?> GetProfile()
        {
            try
            {
                var profile = await _context.Persons
                    .OrderByDescending(x => x.CreatedDate)
                        .Include(x => x.Addresses
                            .Where(y => y.IsActive))
                        .Include(x => x.Employers
                            .Where(x => x.IsActive))
                                .ThenInclude(x => x.WorkExperience
                                    .Where(x => x.IsActive))
                    .FirstOrDefaultAsync(x => x.IsActive);

                return profile;
            }
            catch
            {
                throw;
            }
        }

        // https://stackoverflow.com/questions/27176014/how-to-add-update-child-entities-when-updating-a-parent-entity-in-ef
        public async Task<Person?> UpdateProfile(Person request)
        {
            try
            {
                if (request != null && request.Id != default)
                {
                    var currentProfile = await GetProfileById(request.Id);
                    if (currentProfile != null)
                    {
                        var updatedPerson = _context.Entry(currentProfile);
                        updatedPerson.CurrentValues.SetValues(request);

                        UpSertEntityHelper<Address>
                            .UpSertEntities(_context, request.Addresses, currentProfile.Addresses,
                            (newEntity, old) => { return newEntity.Id == old.Id && old.PersonId == currentProfile.Id; });

                        UpSertEmployers(request.Employers, currentProfile.Employers, currentProfile.Id);

                        updatedPerson.State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        return updatedPerson.Entity;
                    }
                }

                return null;
            }
            catch 
            {
                throw;
            }
        }
        
        public async Task<Person?> GetProfileById(int id)
        {
            try
            {
                var profile = 
                    await _context
                        .Persons
                            .Include(x => x.Addresses
                                .OrderByDescending(x => x.CreatedDate)
                                .Where(y => y.IsActive == true))
                            .Include(x => x.Employers
                                .Where(x => x.IsActive == true))
                                .ThenInclude(x => x.WorkExperience
                                    .Where(x => x.IsActive == true))
                            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true);

                return profile;
            }
            catch
            {
                throw;
            }
        }

        #region Non-Abstract Methods
        private void UpSertEmployers(ICollection<Employer> employers, ICollection<Employer> oldEmployers, int personId)
        {
            if (employers.Count == 0) return;
            foreach (var emp in employers)
            {
                var existing = oldEmployers.Where(x => x.PersonId == personId && x.Id != default && x.Id == emp.Id).SingleOrDefault();
                if (existing != null)
                {
                    UpSertEntityHelper<Employer>.UpdateEntity(_context, emp, existing);

                    var newWorkExp = employers?.Where(x => x.Id == existing.Id)?.SingleOrDefault()?.WorkExperience?.ToList();
                    if (newWorkExp != null && newWorkExp.Count > 0)
                    {
                        UpSertEntityHelper<WorkExperience>
                            .UpSertEntities(_context, newWorkExp, existing.WorkExperience,
                            (request, old) => { return request.EmployerId == existing.Id && old.Id == old.Id; });
                    }
                }
                else
                {
                    UpSertEntityHelper<Employer>.AddEntity(_context, emp, oldEmployers);
                }
            }
        }
        #endregion
    }
}
