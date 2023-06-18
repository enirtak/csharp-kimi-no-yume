using proj_csharp_kiminoyume.DTOs;
using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.Helpers
{
    public class ConvertDTOToModel
    {
        public DreamDictionary? ConvertDictionaryDTOToModel(DreamDictionaryDTO dto)
        {
            if (dto == null) return null;

            var model = new DreamDictionary();
            model.Id = dto.Id;
            model.DreamName = dto.DreamName;
            model.DreamDescription = dto.DreamDescription;
            model.DreamCategoryId = dto.DreamCategoryId;

            return model;
        }

        public List<DreamCategory>? ConvertDreamCategoryDTOToModel(List<DreamCategoryDTO> category)
        {
            if (category.Count == 0) return null;

            var listCategories = new List<DreamCategory>();
            foreach (var cat in category)
            {
                var model = new DreamCategory();
                model.Id = cat.Id;
                model.CategoryName = cat.CategoryName;
                // model.DreamId = cat.DreamDictionaryId.GetValueOrDefault();

                listCategories.Add(model);
            }

            return listCategories;
        }

        public DreamCategory? ConvertCategoryDTOToModel(DreamCategoryDTO dto)
        {
            if (dto == null) return null;

            var model = new DreamCategory();
            model.Id = dto.Id;
            model.CategoryName = dto.CategoryName;

            return model;
        }
    }
}
