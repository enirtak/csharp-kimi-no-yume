namespace proj_csharp_kiminoyume.Helpers
{
    public static class PropertyHelper
    {
        // https://stackoverflow.com/a/17684919
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            try
            {
                obj.GetType().GetProperty(propertyName)!.SetValue(obj, value, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at GetPropertyValue: {ex}");
            }
        }
    }
}
