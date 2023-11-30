using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.Helpers
{
    public static class ConvertDTOToModel
    {
        #region Dream Dictionary
        public static DreamDictionary? ConvertDictionaryDTOToModel(DreamDictionaryDTO dto)
        {
            if (dto == null) return null;

            var model = new DreamDictionary()
            {
                Id = dto.Id,
                DreamName = dto.DreamName,
                DreamDescription = dto.DreamDescription,
                DreamCategoryId = dto.DreamCategoryId,
                IsActive = dto.IsActive.GetValueOrDefault()
            };

            return model;
        }

        #endregion Dream Dictionary

        #region Dream Category
        public static DreamCategory? ConvertCategoryDTOToModel(DreamCategoryDTO dto)
        {
            if (dto == null) return null;

            var model = new DreamCategory()
            {
                Id = dto.Id,
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive.GetValueOrDefault()
            };

            return model;
        }
        #endregion Dream Category

        #region Profile
        public static Person? ConvertPersonDTOToModel(PersonDTO data)
        {
            if (data == null) return null;

            return new Person()
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
                Addresses = ConvertAddressDTOToModel(data.Addresses!)!,
                Employers = ConvertEmployerDTOToModel(data.Employers!)!,
                Skills = ConvertSkillsDTOToModel(data.Skills!)!,
                Projects = ConvertProjectsDTOToModel(data.Projects!)!
            };
        }

        private static List<Address>? ConvertAddressDTOToModel(List<AddressDTO> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<Address>();

            foreach (var d in data)
            {
                list.Add(new Address()
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

        private static List<Employer>? ConvertEmployerDTOToModel(List<EmployerDTO> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<Employer>();

            foreach (var d in data)
            {
                list.Add(new Employer()
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
                    WorkExperience = ConvertWorkExpDTOToModel(d.WorkExps!)!
                });
            }
            return list;
        }

        private static List<WorkExperience>? ConvertWorkExpDTOToModel(List<WorkExperienceDTO> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<WorkExperience>();

            foreach (var d in data)
            {
                list.Add(new WorkExperience()
                {
                    Id = d.Id,
                    EmployerId = d.EmployerId,
                    Description = d.Description,
                    IsActive = d.IsActive
                });
            }

            return list;
        }

        private static List<Skills>? ConvertSkillsDTOToModel(List<SkillsDTO> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<Skills>();

            foreach (var d in data)
            {
                list.Add(new Skills()
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

        private static List<Projects>? ConvertProjectsDTOToModel(List<ProjectsDTO> data)
        {
            if (data == null || data.Count == 0) return null;
            var list = new List<Projects>();

            foreach (var d in data)
            {
                list.Add(new Projects()
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
        #endregion Dream Profile
    }
}
