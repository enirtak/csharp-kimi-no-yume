namespace proj_csharp_kiminoyume.DTOs.JobApplication
{
    public class JobAppCustomFieldDTO: BaseDTO
    {
        public string? CustomFieldName { get; set; }
        public string? FieldNameValue { get; set; }
        public int JobApplicationId { get; set; }
    }
}
