namespace ILIA.SimpleStore.API.Extentions
{
    public static class StringExtentions
    {
        public static string RemoveSentence(this string text, string textToRemove)
        {
            if (String.IsNullOrEmpty(textToRemove))
            {
                return text;
            }
            
            return text.Replace(textToRemove, "");
        }
    }
}
