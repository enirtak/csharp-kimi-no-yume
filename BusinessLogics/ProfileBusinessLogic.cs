using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Services.Profile;
using System.Security.Principal;

namespace proj_csharp_kiminoyume.BusinessLogics
{
    public class ProfileBusinessLogic : IProfileBusinessLogic
    {
        private readonly AppDBContext _dbContext;

        public ProfileBusinessLogic(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Person?> CreateNewProfile(Person request)
        {
            try
            {
                if (request != null)
                {
                    var newProfile = _dbContext.Persons.Add(request);
                    await _dbContext.SaveChangesAsync();
                    return newProfile?.Entity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<List<Person>> GetProfileList(bool getAll = false)
        {
            try
            {
                var profile =
                    await _dbContext.Persons.Where(x => x.IsActive == getAll)
                    .Include(x => x.Addresses.Where(x => x.IsActive == getAll))
                    .Include(x => x.Employers.Where(x => x.IsActive == getAll))
                    .ThenInclude(x => x.WorkExperience.Where(x => x.IsActive == getAll))
                    .Include(x => x.Skills.Where(x => x.IsActive == getAll))
                    .Include(x => x.Projects.Where(x => x.IsActive == getAll))
                    .AsNoTracking()
                    .ToListAsync();


                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Person?> GetProfile()
        {
            try
            {
                var profile = await _dbContext.Persons.OrderByDescending(x => x.CreatedDate).Where(x => x.IsActive.GetValueOrDefault())
                    .Include(x => x.Addresses.OrderByDescending(x => x.CreatedDate).Where(y => y.IsActive.GetValueOrDefault()))
                    .Include(x => x.Employers.Where(x => x.IsActive.GetValueOrDefault()))
                    .ThenInclude(x => x.WorkExperience.Where(x => x.IsActive.GetValueOrDefault()))
                    //.Include(x => x.Skills.Where(x => x.IsActive))
                    //.Include(x => x.Projects.Where(x => x.IsActive))
                    .FirstOrDefaultAsync();

                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        // https://stackoverflow.com/questions/27176014/how-to-add-update-child-entities-when-updating-a-parent-entity-in-ef
        public async Task<Person?> UpdateProfile(Person request)
        {
            try
            {
                if (request != null && request.Id != default)
                {
                    var oldEntity = await GetProfileById(request.Id);
                    if (oldEntity != null)
                    {
                        // IsActive = false will soft delete a record
                     // update parent - Person
                        var updatedPerson = _dbContext.Entry(oldEntity);
                        updatedPerson.CurrentValues.SetValues(request);

                        // create/update children
                        // addresses
                        UpSertEntityHelper<Address>
                            .UpSertEntities(_dbContext, request.Addresses, oldEntity.Addresses,
                            (request, old) => { return request.PersonId == oldEntity.Id && old.Id == old.Id; });

                        // employers & work exps
                        UpSertEmployers(request.Employers, oldEntity.Employers, oldEntity.Id);

                        //// skills
                        //UpSertEntityHelper<Skills>
                        //    .UpSertEntities(_dbContext, request.Skills, oldEntity.Skills, 
                        //    (request,old) => { return request.PersonId == oldEntity.Id && old.Id == old.Id; });

                        //// projects
                        //UpSertEntityHelper<Projects>
                        //    .UpSertEntities(_dbContext, request.Projects, oldEntity.Projects, 
                        //    (request,old) => { return request.PersonId == oldEntity.Id && old.Id == old.Id; });

                        updatedPerson.State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();
                        return updatedPerson.Entity;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }

            return null;
        }
        
        public async Task<Person?> GetProfileById(int id)
        {
            try
            {
                var profile = 
                    await _dbContext
                        .Persons.Where(x => x.Id == id && x.IsActive.GetValueOrDefault())
                        .Include(x => x.Addresses.OrderByDescending(x => x.CreatedDate).Where(y => y.IsActive.GetValueOrDefault()))
                        .Include(x => x.Employers.Where(x => x.IsActive.GetValueOrDefault()))
                        .ThenInclude(x => x.WorkExperience.Where(x => x.IsActive.GetValueOrDefault()))
                        .FirstOrDefaultAsync();

                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
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
                    UpSertEntityHelper<Employer>.UpdateEntity(_dbContext, emp, existing);

                    var newWorkExp = employers?.Where(x => x.Id == existing.Id)?.SingleOrDefault()?.WorkExperience?.ToList();
                    if (newWorkExp != null && newWorkExp.Count > 0)
                    {
                        UpSertEntityHelper<WorkExperience>
                            .UpSertEntities(_dbContext, newWorkExp, existing.WorkExperience,
                            (request, old) => { return request.EmployerId == existing.Id && old.Id == old.Id; });
                    }
                }
                else
                {
                    UpSertEntityHelper<Employer>.AddEntity(_dbContext, emp, oldEmployers);
                }
            }
        }
        #endregion
    }
}
