using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.Helpers
{
    public class ConvertModelToDTO
    {
        public DreamDictionaryDTO? ConvertDictionaryModelToDTO(DreamDictionary dictionary)
        {
            if (dictionary == null) return null;

            var dto = new DreamDictionaryDTO();
            dto.Id = dictionary.Id;
            dto.DreamName = dictionary.DreamName;
            dto.DreamDescription = dictionary.DreamDescription;
            dto.DreamCategoryId = dictionary.DreamCategoryId;

            return dto;
        }

        public List<DreamCategoryDTO>? ConvertDreamCategoryModelToDTO(IList<DreamCategory> category)
        {
            if (category.Count() == 0) return null;

            var listCategories = new List<DreamCategoryDTO>();
            foreach (var cat in category)
            {
                var dto = new DreamCategoryDTO();
                dto.Id = cat.Id;
                dto.CategoryName = cat.CategoryName;
                // dto.DreamDictionaryId = cat.DreamId;

                listCategories.Add(dto);
            }

            return listCategories;
        }

        public DreamCategoryDTO? ConvertCategoryModelToDTO(DreamCategory model)
        {
            if (model == null) return null;

            var dto = new DreamCategoryDTO();
            dto.Id = model.Id;
            dto.CategoryName = model.CategoryName;

            return dto;
        }
    }
}
