using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace proj_csharp_kiminoyume.Helpers
{
    public static class ConvertModelToDTO
    {
        #region Dream Dictionary
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

        public static List<DreamDictionaryDTO> ConvertDreamListModelToDTO(List<DreamDictionary> list)
        {
            var categoriesList = new List<DreamDictionaryDTO>();

            if (list == null || list.Count == 0) return categoriesList;

            foreach (var item in list)
            {
                var dream = ConvertDictionaryModelToDTO(item);
                if (dream != null) categoriesList.Add(dream);
            }

            return categoriesList;
        }
        #endregion Dream Dictionary

        #region Dream Category
        public static DreamCategoryDTO? ConvertCategoryModelToDTO(DreamCategory model)
        {
            if (model == null) return null;

            return new DreamCategoryDTO()
            {
                Id = model.Id,
                CategoryName = model.CategoryName,
                Description = model.Description,
                IsActive = model.IsActive
            };
        }

        public static List<DreamCategoryDTO> ConvertCategoryListModelToDTO(List<DreamCategory> list)
        {
            var categoriesList = new List<DreamCategoryDTO>();

            if (list == null || list.Count == 0) return categoriesList;

            foreach (var item in list)
            {
                var category = ConvertCategoryModelToDTO(item);
                if (category != null) categoriesList.Add(category);
            }

            return categoriesList;
        }
        #endregion Dream Category

        #region Person
        public static PersonDTO? ConvertPersonModelToDTO(Person? data)
        {
            if (data == null) return null;

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
                IsActive = data.IsActive,
                Addresses = ConvertAddressModelToDTO(data.Addresses!),
                Employers = ConvertEmployerModelToDTO(data.Employers!),
                Skills = ConvertSkillsModelToDTO(data.Skills!),
                Projects = ConvertProjectsModelToDTO(data.Projects!)
            };
        }

        private static List<AddressDTO>? ConvertAddressModelToDTO(ICollection<Address> data)
        {
            if (data.Count == 0) return null;

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
                    IsActive = d.IsActive
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
                    IsActive = d.IsActive,
                    WorkExps = ConvertWorkExpModelToDTO(d.WorkExperience)!
                });
            }
            return list;
        }

        private static List<WorkExperienceDTO>? ConvertWorkExpModelToDTO(ICollection<WorkExperience> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<WorkExperienceDTO>();

            foreach (var d in data)
            {
                list.Add(new WorkExperienceDTO()
                {
                    Id = d.Id,
                    EmployerId = d.EmployerId,
                    Description = d.Description,
                    IsActive = d.IsActive
                });
            }

            return list;
        }

        private static List<SkillsDTO>? ConvertSkillsModelToDTO(ICollection<Skills> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<SkillsDTO>();

            foreach (var d in data)
            {
                list.Add(new SkillsDTO()
                {
                    Id = d.Id,
                    PersonId = d.PersonId,
                    SkillName = d.SkillName,
                    SkillType = d.SkillType,
                    IsActive = d.IsActive
                });
            }

            return list;
        }

        private static List<ProjectsDTO>? ConvertProjectsModelToDTO(ICollection<Projects> data)
        {
            if (data == null || data.Count == 0) return null;
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
                    IsActive = d.IsActive
                });
            }

            return list;
        }

        public static List<PersonDTO>? ConvertPersonListModelToDTO(List<Person> data)
        {
            if (data == null || data.Count == 0) return null;
            var result = new List<PersonDTO>();

            if (data.Count > 0)
            {
                foreach (var person in data)
                {
                    var personDTO = ConvertPersonModelToDTO(person);
                    if (personDTO != null)
                    {
                        result.Add(personDTO);
                    }
                }
            }

            return result;
        }
        #endregion Person
    }
}
