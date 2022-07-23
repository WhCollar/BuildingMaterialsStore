namespace TsentrstroyAPI.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAccent(this string txt) 
        { 
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt); 
            return System.Text.Encoding.ASCII.GetString(bytes); 
        }
    }
}