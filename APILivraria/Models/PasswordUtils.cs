using System.Text.RegularExpressions;

namespace APILivraria.Models
{
    public static class PasswordUtils
    {
        public static bool HasSpecialCharacter(string password)
        {
            return Regex.IsMatch(password, @"[\W_]");
        }
    }
}
