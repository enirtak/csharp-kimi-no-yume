using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    // https://stackoverflow.com/questions/27176014/how-to-add-update-child-entities-when-updating-a-parent-entity-in-ef
    public class ProfileBusinessLogic: IEntityRetrievalBusinessLogic<Person>
    {
        private readonly AppDBContext _context;

        public ProfileBusinessLogic(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetById(int id)
        {
            try
            {
                var result = await GetList();

                if (id == default)
                {
                    var latestProfile = result.SingleOrDefault(x => x.IsActive);
                    return latestProfile;
                }

                var profile = result.SingleOrDefault(x => x.IsActive && x.Id == id);
                return profile;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Person>> GetList()
        {
            try
            {
                var profile = 
                    await _context
                        .Persons
                            .AsNoTracking()
                            .OrderByDescending(x => x.CreatedDate)
                                .ThenByDescending(x => x.LastUpdatedDate)
                                .Include(y => y.Addresses
                                    .Where(y => y.IsActive))
                                .Include(emp => emp.Employers
                                    .Where(emp => emp.IsActive))
                                        .ThenInclude(we => we.WorkExperience
                                            .Where(we => we.IsActive))
                            .ToListAsync();

                return profile;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Person?> Create(Person request)
        {
            if (request == null) return null;

            try
            {
                var newProfile = _context.Persons.Add(request);
                await _context.SaveChangesAsync();

                return newProfile?.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Person?> Update(Person request)
        {
            if (request == null || request?.Id == default) return null;

            try
            {
                var oldEntity = await GetById(request!.Id);
                if (oldEntity == null) return null;

                var updatedPerson = _context.Entry(oldEntity);
                updatedPerson.CurrentValues.SetValues(request);

                UpSertEntityHelper<Address>
                    .UpSertEntities(
                        _context, 
                        request.Addresses, 
                        oldEntity.Addresses,
                        (request, old) => 
                        { 
                            return request.PersonId == oldEntity.Id && old.Id == old.Id; 
                        });

                UpSertEmployers(request.Employers, oldEntity.Employers, oldEntity.Id);

                updatedPerson.State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return updatedPerson.Entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            if (id == default) return;

            try
            {
                await _context.Persons
                    .Where(x => x.Id == id)
                    .ExecuteUpdateAsync(update =>
                        update.SetProperty(person => person.IsActive, false));
            }
            catch
            {
                throw;
            }
        }

        #region Non-Abstract Methods
        public PersonDTO? CreateDummyPersonRequest()
        {
            return new PersonDTO()
            {
                FirstName = "FN",
                LastName = "LN",
                PhoneNumber = "123456789",
                EmailAddress = "test@mail.com",
                GitHub = "GitHub.com",
                LinkedIn = "LinkedIn.com",
                AboutMe = "About Me",
                Addresses = new List<AddressDTO> {
                    new AddressDTO()
                    {
                        Id = 4,
                        PersonId = 4,
                        Address1 = "218 Conaway Rd",
                        City = "Bloomingdale",
                        State = "GA",
                        Zip = "31302",
                        Country = "USA"
                    }
                },
                Employers = new List<EmployerDTO> {
                    new EmployerDTO()
                    {
                        CompanyName = "Test Company 1",
                        Position = "Position 1",
                        Salary = 1000,
                        Address1 = "Employee Address1",
                        City = "City",
                        State = "",
                        Zip = "1605",
                        Country = "PH",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        WorkExps = new List<WorkExperienceDTO>
                        {
                            new WorkExperienceDTO()
                            {
                                Description = "Description 1",
                            },
                            new WorkExperienceDTO()
                            {
                                Description = "Description 2",
                            },
                            new WorkExperienceDTO()
                            {
                                Description = "Description 3",
                            }
                        }
                    }
                },
                Skills = new List<SkillsDTO> {
                    new SkillsDTO()
                    {
                        SkillName = "Skill 1",
                        SkillType = 1
                    },
                    new SkillsDTO()
                    {
                        SkillName = "Skill 1",
                        SkillType = 2
                    }
                },
                Projects = new List<ProjectsDTO> {
                    new ProjectsDTO()
                    {
                        ProjectName = "Project 1",
                        ProjectDescription = "Project 1 Description"
                    },
                    new ProjectsDTO()
                    {
                        ProjectName = "Project 2",
                        ProjectDescription = "Project 2 Description"
                    }
                }
            };
        }
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
