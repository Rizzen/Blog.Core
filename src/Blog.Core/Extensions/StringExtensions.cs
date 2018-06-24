using System.Linq;
using System.Text;

namespace Blog.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string ToUnderscoreCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x)
                                                   ? "_" + x.ToString()
                                                   : x.ToString())).ToLowerInvariant();
        }
        
        public static string RemoveSpecialCharacters(this string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if ((c >= '0' && c <= '9')
                    || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z')
                    || (c >= 'а' && c <= 'я')
                    || (c >= 'А' && c <= 'Я')
                    || (c == '.' || c == '_'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}