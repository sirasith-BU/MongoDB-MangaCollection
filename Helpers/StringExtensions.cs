// ที่ไหนก็ได้ในโปรเจกต์ (เช่น Helpers/StringExtensions.cs)
using System.Security.Cryptography;
using System.Text;

namespace MangaAPI.Helpers
{
    public static class StringExtensions
    {
        public static string ToSHA256Hash(this string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = SHA256.HashData(bytes);

            var sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2")); // แปลง byte → hex string

            return sb.ToString();
        }
    }
}
