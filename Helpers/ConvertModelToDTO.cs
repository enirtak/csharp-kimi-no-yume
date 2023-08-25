using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.Helpers
{
    public static class ConvertModelToDTO
    {
        public static DreamDictionaryDTO? ConvertDictionaryModelToDTO(DreamDictionary dictionary)
        {
            if (dictionary == null) return null;

            return new DreamDictionaryDTO()
            {
                Id = dictionary.Id,
                DreamName = dictionary.DreamName,
                DreamDescription = dictionary.DreamDescription,
                DreamCategoryId = dictionary.DreamCategoryId
            };
        }

        public static DreamCategoryDTO? ConvertCategoryModelToDTO(DreamCategory model)
        {
            if (model == null) return null;

            return new DreamCategoryDTO()
            {
                Id = model.Id,
                CategoryName = model.CategoryName,
                Description = model.Description,
                IsActive = model.IsActive.GetValueOrDefault()
            };
        }

        public static PersonDTO ConvertPersonModelToDTO(Person? data)
        {
            if (data == null) return null!;

            return new PersonDTO()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                AboutMe = data.AboutMe,
                EmailAddress = data.EmailAddress,
                LinkedIn = data.LinkedIn,
                GitHub = data.GitHub,
                Other = data.Other,
                PhoneNumber = data.PhoneNumber,
                WebsiteUrl = data.WebsiteUrl,
                IsActive = data.IsActive.GetValueOrDefault(),
                Addresses = ConvertAddressModelToDTO(data.Addresses!),
                Employers = ConvertEmployerModelToDTO(data.Employers!),
                Skills = ConvertSkillsModelToDTO(data.Skills!),
                Projects = ConvertProjectsModelToDTO(data.Projects!)
            };
        }

        private static List<AddressDTO> ConvertAddressModelToDTO(ICollection<Address> data)
        {
            if (data.Count == 0) return null!;

            var list = new List<AddressDTO>();
            foreach (var d in data)
            {
                list.Add(new AddressDTO()
                {
                    Id = d.Id,
                    PersonId = d.PersonId,
                    Address1 = d.Address1,
                    Address2 = d.Address2,
                    City = d.City,
                    State = d.State,
                    Zip = d.Zip,
                    Country = d.Country,
                    IsActive = d.IsActive.GetValueOrDefault()
                });
            }

            return list;
        }

        private static List<EmployerDTO> ConvertEmployerModelToDTO(ICollection<Employer> data)
        {
            if (data == null) return null!;

            var list = new List<EmployerDTO>();
            foreach (var d in data)
            {
                list.Add(new EmployerDTO()
                {
                    Id = d.Id,
                    PersonId = d.PersonId,
                    CompanyName = d.CompanyName,
                    Position = d.Position,
                    Salary = d.Salary,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Address1 = d.Address1,
                    Address2 = d.Address2,
                    City = d.City,
                    State = d.State,
                    Zip = d.Zip,
                    Country = d.Country,
                    IsActive = d.IsActive.GetValueOrDefault(),
                    WorkExps = ConvertWorkExpModelToDTO(d.WorkExperience!)
                });
            }
            return list;
        }

        private static List<WorkExperienceDTO> ConvertWorkExpModelToDTO(ICollection<WorkExperience> data)
        {
            if (data == null) return null!;

            var list = new List<WorkExperienceDTO>();
            foreach (var d in data)
            {
                list.Add(new WorkExperienceDTO()
                {
                    Id = d.Id,
                    EmployerId = d.EmployerId,
                    Description = d.Description,
                    IsActive = d.IsActive.GetValueOrDefault()
                });
            }

            return list;
        }

        private static List<SkillsDTO> ConvertSkillsModelToDTO(ICollection<Skills> data)
        {
            if (data == null) return null!;

            var list = new List<SkillsDTO>();
            foreach (var d in data)
            {
                list.Add(new SkillsDTO()
                {
                    Id = d.Id,
                    PersonId = d.PersonId,
                    SkillName = d.SkillName,
                    SkillType = d.SkillType,
                    IsActive = d.IsActive.GetValueOrDefault()
                });
            }

            return list;
        }

        private static List<ProjectsDTO> ConvertProjectsModelToDTO(ICollection<Projects> data)
        {
            if (data == null) return null!;

            var list = new List<ProjectsDTO>();
            foreach (var d in data)
            {
                list.Add(new ProjectsDTO()
                {
                    Id = d.Id,
                    PersonId = d.PersonId,
                    ProjectName = d.ProjectName,
                    ProjectDescription = d.ProjectDescription,
                    ProjectStatus = d.ProjectStatus,
                    IsActive = d.IsActive.GetValueOrDefault()
                });
            }

            return list;
        }

        public static List<PersonDTO?> ConvertPersonModelToDTO(List<Person> persons)
        {
            var result = new List<PersonDTO?>();
            if (persons.Count > 0)
            {
                foreach (var person in persons)
                {
                    var personDTO = ConvertModelToDTO.ConvertPersonModelToDTO(person);
                    if (personDTO != null)
                    {
                        result.Add(personDTO);
                    }
                }
            }

            return result;
        }
    }
}
