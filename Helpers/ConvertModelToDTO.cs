using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.DTOs.JobApplication;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Models.JobApplication;
using System.Collections.Generic;

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
                IsActive = model.IsActive
            };
        }

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

        public static List<PersonDTO>? ConvertPersonModelToDTO(List<Person> data)
        {
            if (data == null || data.Count == 0) return null;
            var result = new List<PersonDTO>();

            foreach (var person in data)
            {
                var personDTO = ConvertPersonModelToDTO(person);
                if (personDTO != null)
                {
                    result.Add(personDTO);
                }
            }

            return result;
        }

        public static List<JobApplicationDTO>? ConvertJobApplicationListModelToDTO(List<JobApplication> data)
        {
            if (data == null || data.Count == 0) return null;
            var result = new List<JobApplicationDTO>();

            foreach (var job in data)
            {
                result.Add(ConvertJobModelToDTO(job));
            }

            return result;
        }

        public static JobApplicationDTO ConvertJobModelToDTO(JobApplication data)
        {
            var customFields = data.CustomFields?.Count > 0 ? ConvertJobAppFieldsModelToDTO(data.CustomFields) : null;

            return new JobApplicationDTO()
            {
                Id = data.Id,
                JobTitle = data.JobTitle,
                CompanyName = data.CompanyName,
                CompanyWebsite = data.CompanyWebsite,
                WorkLocation = data.WorkLocation,
                WorkArrangement = data.WorkArrangement,
                ApplicationDate = data.ApplicationDate,
                ApplicationStatus = data.ApplicationStatus,
                ApplicationEmail = data.ApplicationEmail,
                ApplicationSource = data.ApplicationSource,
                JobType = data.JobType,
                Salary = data.Salary,
                IsActive = data.IsActive,
                CustomFields = customFields
            };
        }

        public static List<JobAppCustomFieldDTO>? ConvertJobAppFieldsModelToDTO(ICollection<JobAppCustomField> data)
        {
            if (data == null || data.Count == 0) return null;
            var result = new List<JobAppCustomFieldDTO>();

            foreach (var fields in data)
            {
                result.Add(new JobAppCustomFieldDTO()
                {
                    Id = fields.Id,
                    JobApplicationId = fields.JobApplicationId,
                    CustomFieldName = fields.CustomFieldName,
                    FieldNameValue = fields.FieldNameValue,
                    IsActive = fields.IsActive
                });
            }

            return result;
        }
    }
}
